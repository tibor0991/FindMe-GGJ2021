using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace io.github.tibor0991 {
    public class GameManager : MonoBehaviour, IOnEventCallback
    {
        public List<int> playersViewID = new List<int>();

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
            switch (photonEvent.Code)
            {
                case 1:
                    playersViewID.Add((int)photonEvent.CustomData);
                    break;
            }
        }
    }
}