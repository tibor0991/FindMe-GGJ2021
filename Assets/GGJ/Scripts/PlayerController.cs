using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

    void OnEnable()
    {
        if(m_Animator == null)
        {
            m_Animator = GetComponent<Animator>();
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
        m_Animator.SetFloat("Forward", dir3D.magnitude, 0.2f, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir3D, Vector3.up), 100f * Time.deltaTime);
    }
}
