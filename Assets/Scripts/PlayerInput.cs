using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Animator animator;
    private int joystickNumber; // Número del mando asignado
    private ShootAttack shootAttack; // Referencia al script ShootAttack

    public Transform otherPlayer;

    private float zMin = -7f;
    private float zMax = 7f;

    // Asignar el número del joystick a este jugador
    public void SetJoystickNumber(int number)
    {
        joystickNumber = number;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        shootAttack = GetComponentInChildren<ShootAttack>();
    }

    private void Update()
    {
        if (joystickNumber == 1) InputPlayer1();
        else InputPlayer2();

        RestrictMovement();
    }

    void RestrictMovement()
    {
        if (otherPlayer == null) return;

        Vector3 currentPosition = transform.position;
        Vector3 otherPlayerPosition = otherPlayer.position;

        // Limitar el movimiento basado en los límites del mapa
        currentPosition.z = Mathf.Clamp(currentPosition.z, zMin, zMax);

        // Limitar el movimiento basado en la posición del otro jugador
        if (joystickNumber == 1)
        {
            // Player 1 no puede ir más a la derecha que Player 2
            currentPosition.z = Mathf.Min(currentPosition.z, otherPlayerPosition.z);
        }
        else if (joystickNumber == 2)
        {
            // Player 2 no puede ir más a la izquierda que Player 1
            currentPosition.z = Mathf.Max(currentPosition.z, otherPlayerPosition.z);
        }

        // Actualizar la posición del jugador
        transform.position = currentPosition;
    }

    void InputPlayer1()
    {
        // Obtener valores de los sticks analógicos
        float horizontal = Input.GetAxis("Joystick1Horizontal");
        float vertical = Input.GetAxis("Joystick1Vertical");

        // Determinar si el personaje está caminando hacia adelante o hacia atrás
        animator.SetBool("IsWalkingForward", horizontal > 0.5);
        animator.SetBool("IsWalkingBackward", horizontal < -0.5);

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
        if (Input.GetButtonDown("Joystick1ButtonX"))
        {
            animator.SetTrigger("QuickAttack");
        }
        else if (Input.GetButtonDown("Joystick1ButtonY"))
        {
            if (shootAttack != null && shootAttack.GetCurrentAmmo() > 0)
            {
                animator.SetTrigger("SlowAttack");
            }
        }

        if (Input.GetButtonDown("Joystick1ButtonA"))
        {
            animator.SetTrigger("LowQuickAttack");
        }
        else if (Input.GetButtonDown("Joystick1ButtonB"))
        {
            animator.SetTrigger("LowSlowAttack");
        }
    }

    void InputPlayer2()
    {
        // Obtener valores de los sticks analógicos
        float horizontal = Input.GetAxis("Joystick2Horizontal");
        float vertical = Input.GetAxis("Joystick2Vertical");

        // Determinar si el personaje está caminando hacia adelante o hacia atrás
        animator.SetBool("IsWalkingForward", horizontal > 0.5);
        animator.SetBool("IsWalkingBackward", horizontal < -0.5);

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
        if (Input.GetButtonDown("Joystick2ButtonX"))
        {
            animator.SetTrigger("QuickAttack");
        }
        else if (Input.GetButtonDown("Joystick2ButtonY"))
        {
            if (shootAttack != null && shootAttack.GetCurrentAmmo() > 0)
            {
                animator.SetTrigger("SlowAttack");
            }
        }

        if (Input.GetButtonDown("Joystick2ButtonA"))
        {
            animator.SetTrigger("LowQuickAttack");
        }
        else if (Input.GetButtonDown("Joystick2ButtonB"))
        {
            animator.SetTrigger("LowSlowAttack");
        }
    }
}
