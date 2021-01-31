using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingAreaSpawner : MonoBehaviour
{
    public GameObject TriggerAreaPrefab;

    public Transform[] Hexes;

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(TriggerAreaPrefab.name, Hexes[Random.Range(0, Hexes.Length)].position, Quaternion.identity);
        }
    }
}
