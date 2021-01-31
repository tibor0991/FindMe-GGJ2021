using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace io.github.tibor0991 {
    public enum EventCodes { OnPlayerPrefabInstance = 1, OnMeetingAreaEntered = 2, OnMeetingButtonPressed = 3};

    public class GameManager : MonoBehaviour, IOnEventCallback
    {
        public List<PlayerController> players = new List<PlayerController>();

        public AudioClip MeetingAreaSFX;
        public AudioClip MeetingButtonPressedSFX;
        public AudioSource GlobalSFX;

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

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
                    MeetingButtonsPressedCounter++;
                    if (MeetingButtonsPressedCounter == 2 && !GameOver)
                    {
                        if (players[0].IsReadyForMeeting() && players[1].IsReadyForMeeting())
                        {
                            Debug.Log("WIN!");
                            
                        }
                        else
                        {
                            Debug.Log("LOST!");
                        }
                        GameOver = true;
                    }
                    break;
            }
        }
    }
}