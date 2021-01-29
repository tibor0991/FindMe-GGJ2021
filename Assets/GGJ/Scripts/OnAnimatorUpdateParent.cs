using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimatorUpdateParent : MonoBehaviour
{
    public Transform Parent;
    
    [SerializeField]
    private Animator m_Animator;

    private void OnAnimatorMove()
    {
        Parent.position += m_Animator.deltaPosition;
    }
}
