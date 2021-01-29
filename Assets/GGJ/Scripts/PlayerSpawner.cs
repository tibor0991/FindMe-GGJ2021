using Cinemachine;
using Photon.Pun;
using UnityEngine;


public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PlayerPrefab;

    [SerializeField]
    private CinemachineVirtualCamera m_PlayerCamera;

    // Start is called before the first frame update
    void Start()
    {
        var instance = PhotonNetwork.Instantiate(m_PlayerPrefab.name, Vector3.zero, Quaternion.identity);
        m_PlayerCamera.Follow = instance.transform;

    }

}
