using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Obtener valores de los sticks analógicos
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Determinar si el personaje está caminando hacia adelante o hacia atrás
        animator.SetBool("IsWalkingForward", horizontal > 0);
        animator.SetBool("IsWalkingBackward", horizontal < 0);

        // Esquivas
        if (vertical > 0.5f)
        {
            animator.SetTrigger("DodgeHigh");
        }
        else if (vertical < -0.5f)
        {
            animator.SetTrigger("DodgeLow");
        }

        // Ataques
        if (Input.GetButtonDown("QuickAttack"))
        {
            animator.SetTrigger("QuickAttack");
        }
        else if (Input.GetButtonDown("SlowAttack"))
        {
            animator.SetTrigger("SlowAttack");
        }

        if (Input.GetButtonDown("LowQuickAttack"))
        {
            animator.SetTrigger("LowQuickAttack");
        }
        else if (Input.GetButtonDown("LowSlowAttack"))
        {
            animator.SetTrigger("LowSlowAttack");
        }
    }
}
