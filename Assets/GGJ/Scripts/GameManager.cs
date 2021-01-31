using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace io.github.tibor0991 {
    public enum EventCodes { OnPlayerPrefabInstance = 1, OnMeetingAreaEntered = 2};

    public class GameManager : MonoBehaviour, IOnEventCallback
    {
        public List<int> playersViewID = new List<int>();

        public AudioClip MeetingAreaAudioClip;
        public AudioSource GlobalSFX;

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void OnEvent(EventData photonEvent)
        {
            switch ((EventCodes)photonEvent.Code)
            {
                case EventCodes.OnPlayerPrefabInstance:
                    playersViewID.Add((int)photonEvent.CustomData);
                    break;
                case EventCodes.OnMeetingAreaEntered:
                    GlobalSFX.PlayOneShot(MeetingAreaAudioClip);
                    break;
            }
        }

        [SerializeField]
        private float m_MinDistanceForMeeting = 4f;

        public void HandleMeeting()
        {
            var playerA = PhotonView.Find(playersViewID[0]).GetComponent<PlayerController>();
            var playerB = PhotonView.Find(playersViewID[1]).GetComponent<PlayerController>();
        }
    }
}