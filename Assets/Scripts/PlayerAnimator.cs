using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    public AnimationEventSearcher animationEvent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Entry"); // Reproduce Entry al inicio
    }

    public void SetIdle()
    {
        animator.SetBool("IsWalkingForward", false);
        animator.SetBool("IsWalkingBackward", false);
        //animator.Play("Idle");
    }

    public void PlayHit()
    {
        animator.SetTrigger("Hit");
    }

    public void PlayWin()
    {
        animator.SetTrigger("Win");
        StartCoroutine(ResetCamera());
    }

    public void PlayDie()
    {
        animator.SetTrigger("Die");
    }

    private IEnumerator ResetCamera()
    {
        yield return new WaitForSeconds(5.0f);

        animationEvent.DeactivateCamera();
    }
}

