using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Animator animator;
    private int joystickNumber; // N�mero del mando asignado
    private ShootAttack shootAttack; // Referencia al script ShootAttack

    [HideInInspector] public Transform otherPlayer;

    private float zMin = -7f;
    private float zMax = 7f;

    private bool isOnAnimation = false;

    // Asignar el n�mero del joystick a este jugador
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
        // Solo aplicar restricciones si los jugadores est�n instanciados
        if (otherPlayer != null)
        {
            Vector3 currentPosition = transform.position;

            // Limitar el movimiento basado en los l�mites del mapa
            currentPosition.z = Mathf.Clamp(currentPosition.z, zMin, zMax);

            //// Evitar que el jugador 1 se mueva m�s all� del jugador 2 y viceversa
            //if (joystickNumber == 1)
            //{
            //    // El jugador 1 no puede moverse m�s all� del jugador 2 (jugador 1 no puede pasar a la derecha del jugador 2)
            //    currentPosition.z = Mathf.Min(currentPosition.z, otherPlayer.position.z);
            //}
            //else if (joystickNumber == 2)
            //{
            //    // El jugador 2 no puede moverse m�s all� del jugador 1 (jugador 2 no puede pasar a la izquierda del jugador 1)
            //    currentPosition.z = Mathf.Max(currentPosition.z, otherPlayer.position.z);
            //}

            // Actualizar la posici�n del jugador
            transform.position = currentPosition;
        }
    }

    void InputPlayer1()
    {
        // Obtener valores de los sticks anal�gicos
        float horizontal = Input.GetAxis("Joystick1Horizontal");
        float vertical = Input.GetAxis("Joystick1Vertical");

        // Determinar si el personaje est� caminando hacia adelante o hacia atr�s
        animator.SetBool("IsWalkingForward", horizontal > 0.5);
        animator.SetBool("IsWalkingBackward", horizontal < -0.5);

        // Esquivas
        if (!isOnAnimation) // Solo permitir esquivar si no est� ya esquivando
        {
            if (vertical > 0.5f)
            {
                animator.SetTrigger("DodgeHigh");
                StartCoroutine(ResetAnimationState(1.04f));
            }
            else if (vertical < -0.5f)
            {
                animator.SetTrigger("DodgeLow");
                StartCoroutine(ResetAnimationState(0.58f));
            }
        }

        // Ataques
        if (!isOnAnimation)
        {
            if (Input.GetButtonDown("Joystick1ButtonX"))
            {
                animator.SetTrigger("QuickAttack");
                StartCoroutine(ResetAnimationState(0.95f));
            }
            else if (Input.GetButtonDown("Joystick1ButtonY"))
            {
                if (shootAttack != null && shootAttack.GetCurrentAmmo() > 0)
                {
                    animator.SetTrigger("SlowAttack");
                    StartCoroutine(ResetAnimationState(2.16f));
                }
            }
        }

        if (!isOnAnimation)
        {
            if (Input.GetButtonDown("Joystick1ButtonA"))
            {
                animator.SetTrigger("LowQuickAttack");
                StartCoroutine(ResetAnimationState(0.79f));
            }
            else if (Input.GetButtonDown("Joystick1ButtonB"))
            {
                animator.SetTrigger("LowSlowAttack");
                StartCoroutine(ResetAnimationState(1.2f));
            }
        }
    }

    void InputPlayer2()
    {
        // Obtener valores de los sticks anal�gicos
        float horizontal = Input.GetAxis("Joystick2Horizontal");
        float vertical = Input.GetAxis("Joystick2Vertical");

        // Determinar si el personaje est� caminando hacia adelante o hacia atr�s
        animator.SetBool("IsWalkingForward", horizontal > 0.5);
        animator.SetBool("IsWalkingBackward", horizontal < -0.5);

        // Esquivas
        if (!isOnAnimation) // Solo permitir esquivar si no est� ya esquivando
        {
            if (vertical > 0.5f)
            {
                animator.SetTrigger("DodgeHigh");
                StartCoroutine(ResetAnimationState(1.04f));
            }
            else if (vertical < -0.5f)
            {
                animator.SetTrigger("DodgeLow");
                StartCoroutine(ResetAnimationState(0.58f));
            }
        }

        // Ataques
        if (!isOnAnimation)
        {
            if (Input.GetButtonDown("Joystick1ButtonX"))
            {
                animator.SetTrigger("QuickAttack");
                StartCoroutine(ResetAnimationState(0.95f));
            }
            else if (Input.GetButtonDown("Joystick1ButtonY"))
            {
                if (shootAttack != null && shootAttack.GetCurrentAmmo() > 0)
                {
                    animator.SetTrigger("SlowAttack");
                    StartCoroutine(ResetAnimationState(2.16f));
                }
            }
        }

        if (!isOnAnimation)
        {
            if (Input.GetButtonDown("Joystick1ButtonA"))
            {
                animator.SetTrigger("LowQuickAttack");
                StartCoroutine(ResetAnimationState(0.79f));
            }
            else if (Input.GetButtonDown("Joystick1ButtonB"))
            {
                animator.SetTrigger("LowSlowAttack");
                StartCoroutine(ResetAnimationState(1.2f));
            }
        }
    }

    private IEnumerator ResetAnimationState(float seconds)
    {
        isOnAnimation = true;

        yield return new WaitForSeconds(seconds);

        isOnAnimation = false;
    }
}
