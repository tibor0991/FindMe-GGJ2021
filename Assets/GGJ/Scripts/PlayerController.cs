using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace io.github.tibor0991
{

    public class PlayerController : MonoBehaviourPun
    {
        [SerializeField]
        private Animator m_Animator;

        public InputActionReference MoveActionReference, JoinOtherActionReference;

        [SerializeField]
        private bool m_ForceInput = false;

        void Start()
        {
            if (photonView.IsMine || m_ForceInput)
            {
                if (m_Animator == null)
                {
                    m_Animator = GetComponent<Animator>();
                }

                MoveActionReference.action.performed += OnMoveInput;
                //JoinOtherActionReference.action.performed metti qui roba;
                MoveActionReference.action.Enable();
            }
        }

        private Vector3 dir3D;
        public void OnMoveInput(InputAction.CallbackContext ctx)
        {
            var dir = ctx.ReadValue<Vector2>();
            dir3D = new Vector3(dir.x, 0, dir.y);
        }

        private void Update()
        {
            if ((!photonView.IsMine && PhotonNetwork.IsConnected) || !m_ForceInput) return;

            m_Animator.SetFloat("Forward", dir3D.magnitude, 0.01f, Time.deltaTime);
            if (dir3D.magnitude > 0)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir3D, Vector3.up), 100f * Time.deltaTime);
            }
        }
    }
}