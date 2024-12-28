using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        animator.SetBool("IsWalkingForward", false);
        animator.SetBool("IsWalkingBackward", false);
    }

    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }

    public void PlayWin()
    {
        animator.SetTrigger("Win");
    }

    public void PlayDie()
    {
        animator.SetTrigger("Die");
    }
}

