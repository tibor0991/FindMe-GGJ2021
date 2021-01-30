using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChooser : MonoBehaviour
{
    public GameObject Map1, Map2;

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Map1.SetActive(true);
            Map2.SetActive(false);
        }
        else
        {
            Map1.SetActive(false);
            Map2.SetActive(true);
        }
    }
}
