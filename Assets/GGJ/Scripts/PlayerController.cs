using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

namespace io.github.tibor0991
{

    public class PlayerController : MonoBehaviourPun
    {
        [SerializeField]
        private Animator m_Animator;

        public InputActionReference MoveActionReference, JoinOtherActionReference;

        [SerializeField]
        private float m_TurnSpeed = 50f;

        [Header("Shouting")]
        [SerializeField]
        private VisualEffect ShoutEffect;

        [SerializeField]
        private AudioSource ShoutEffectSFX;

        [SerializeField]
        private NavMeshAgent m_Agent;

        [SerializeField]
        private Renderer playerMesh;
        public Material MasterFoxMaterial, ClientFoxMaterial;

        [SerializeField]
        private ParticleSystem FootSteps;

        [SerializeField]
        private AudioSource m_PlayerSoundboard;

        [SerializeField]
        private AudioClip m_OnOtherEnteredMeetingArea;

        void Start()
        {
            if (photonView.IsMine)
            {
                if (m_Animator == null)
                {
                    m_Animator = GetComponent<Animator>();
                }

                MoveActionReference.action.performed += OnMoveInput;
                MoveActionReference.action.Enable();

                JoinOtherActionReference.action.performed += OnJoinAttempt;
                JoinOtherActionReference.action.Enable();
            }

            //Set player color, Red if Master, Gray if client
            playerMesh.material = (photonView.Controller.IsMasterClient)? MasterFoxMaterial : ClientFoxMaterial;

            //
            if(!photonView.IsMine)
            {
                FootSteps.Play();
                playerMesh.enabled = false;
            }
        }

        private Vector3 dir3D;
        public void OnMoveInput(InputAction.CallbackContext ctx)
        {
            var dir = ctx.ReadValue<Vector2>();

            dir3D = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 45, 0), Vector3.one) * new Vector3(dir.x, 0, dir.y);
        }

        public void OnJoinAttempt(InputAction.CallbackContext ctx)
        {
            Shout();
        }

        [PunRPC]
        public void Shout()
        {
            if(photonView.IsMine)
            {
                ShoutEffect.Play();
            }
            Debug.Log("There's a shout");
        }

        private void Update()
        {
            if ((!photonView.IsMine && PhotonNetwork.IsConnected)) return;

            m_Animator.SetFloat("Forward", dir3D.magnitude, 0.2f, Time.deltaTime);
            if (dir3D.magnitude > 0)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir3D, Vector3.up), m_TurnSpeed * Time.deltaTime);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("MeetingArea") && !photonView.IsMine) 
            {
                Debug.Log("Something got inside the meeting area");
                WarnOther();
            }
        }

        [PunRPC]
        public void WarnOther()
        {
            if (photonView.IsMine)
            {
                m_PlayerSoundboard.PlayOneShot(m_OnOtherEnteredMeetingArea);
            }
        }
    }
}