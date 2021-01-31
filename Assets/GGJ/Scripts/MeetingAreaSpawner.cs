using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingAreaSpawner : MonoBehaviour
{
    public GameObject TriggerAreaPrefab;

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(TriggerAreaPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}
