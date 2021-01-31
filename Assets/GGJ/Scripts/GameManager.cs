using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace io.github.tibor0991 {
    public enum EventCodes { OnPlayerPrefabInstance = 1, OnMeetingAreaEntered = 2, OnMeetingButtonPressed = 3};

    public class GameManager : MonoBehaviourPunCallbacks
    {
        public List<PlayerController> players = new List<PlayerController>();

        public AudioClip MeetingAreaSFX;
        public AudioClip MeetingButtonPressedSFX;
        public AudioSource GlobalSFX;

        public AudioClip OnSuccessSFX, OnFailSFX;

        private void Start()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }

        public Bolt.StateMachine UI_SM;

        public int MeetingButtonsPressedCounter = 0;
        public bool GameOver = false;

        public void OnEvent(EventData photonEvent)
        {
            switch ((EventCodes)photonEvent.Code)
            {
                case EventCodes.OnPlayerPrefabInstance:
                    var playerView = PhotonView.Find((int)photonEvent.CustomData);
                    players.Add(playerView.GetComponent<PlayerController>());
                    break;
                case EventCodes.OnMeetingAreaEntered:
                    GlobalSFX.PlayOneShot(MeetingAreaSFX);
                    break;
                case EventCodes.OnMeetingButtonPressed:
                    GlobalSFX.PlayOneShot(MeetingButtonPressedSFX);
                    OnPressedMeetingButton();
                    break;
            }
        }


        
        public void OnPressedMeetingButton()
        {
            MeetingButtonsPressedCounter++;
            if (MeetingButtonsPressedCounter == 2 && !GameOver)
            {
                if (players[0].IsReadyForMeeting() && players[1].IsReadyForMeeting())
                {
                    Debug.Log("WIN!");
                    GlobalSFX.PlayOneShot(OnSuccessSFX);
                    players[0].Reveal();
                    players[1].Reveal();
                    UI_SM.TriggerUnityEvent("OnVictory");
                }
                else
                {
                    Debug.Log("LOST!");
                    GlobalSFX.PlayOneShot(OnFailSFX);
                    UI_SM.TriggerUnityEvent("OnLoss");
                }
                GameOver = true;
            }
        }

        public void GetBackToMainMenu()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            PhotonNetwork.LoadLevel(0);
        }
    }
}