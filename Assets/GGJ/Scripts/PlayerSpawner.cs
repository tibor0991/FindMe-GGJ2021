using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace io.github.tibor0991
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_PlayerPrefab;

        [SerializeField]
        private CinemachineVirtualCamera m_PlayerCamera;

        public UnityEvent<GameObject> OnPlayerInstanced;

        public float SpawnAreaRadius = 10f;

        // Start is called before the first frame update
        void Start()
        {
            Vector2 planarRandomSourcePosition = Random.insideUnitCircle * SpawnAreaRadius;
            Vector3 randomSourcePos = new Vector3(planarRandomSourcePosition.x, 0, planarRandomSourcePosition.y);
            NavMeshHit hit;
            NavMesh.SamplePosition(randomSourcePos, out hit, 1f, NavMesh.AllAreas);
            var instance = PhotonNetwork.Instantiate(m_PlayerPrefab.name, hit.position, Quaternion.identity);
            m_PlayerCamera.Follow = instance.transform;
            OnPlayerInstanced.Invoke(instance);
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, 0f, 1f));
            Gizmos.DrawWireSphere(Vector3.zero, SpawnAreaRadius);
        }

    }
}
