using Cinemachine;
using Photon.Pun;
using UnityEngine;


public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PlayerPrefab;

    [SerializeField]
    private CinemachineVirtualCamera m_PlayerCamera;

    public GameObject Map1, Map2;

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Map1.SetActive(true);
            Map2.SetActive(false);
        }
        else
        {
            Map1.SetActive(false);
            Map2.SetActive(true);
        }

        var instance = PhotonNetwork.Instantiate(m_PlayerPrefab.name, Vector3.zero, Quaternion.identity);
        m_PlayerCamera.Follow = instance.transform;




    }

}
