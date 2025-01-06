using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Animator animator;
    private int joystickNumber; // Número del mando asignado
    private ShootAttack shootAttack; // Referencia al script ShootAttack

    [HideInInspector] public Transform otherPlayer;

    private float zMin = -7f;
    private float zMax = 7f;

    private bool isOnAnimation = false;

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
        HandleInput();
        RestrictMovement();
    }

    void RestrictMovement()
    {
        // Solo aplicar restricciones si los jugadores están instanciados
        if (otherPlayer != null)
        {
            Vector3 currentPosition = transform.position;

            // Limitar el movimiento basado en los límites del mapa
            currentPosition.z = Mathf.Clamp(currentPosition.z, zMin, zMax);

            //// Evitar que el jugador 1 se mueva más allá del jugador 2 y viceversa
            //if (joystickNumber == 1)
            //{
            //    // El jugador 1 no puede moverse más allá del jugador 2 (jugador 1 no puede pasar a la derecha del jugador 2)
            //    currentPosition.z = Mathf.Min(currentPosition.z, otherPlayer.position.z);
            //}
            //else if (joystickNumber == 2)
            //{
            //    // El jugador 2 no puede moverse más allá del jugador 1 (jugador 2 no puede pasar a la izquierda del jugador 1)
            //    currentPosition.z = Mathf.Max(currentPosition.z, otherPlayer.position.z);
            //}

            // Actualizar la posición del jugador
            transform.position = currentPosition;
        }
    }

    void HandleInput()
    {
        // Obtener valores dinamicamente segun el joystick asignado
        string horizontalAxis = $"Joystick{joystickNumber}Horizontal";
        string verticalAxis = $"Joystick{joystickNumber}Vertical";
        string buttonX = $"Joystick{joystickNumber}ButtonX";
        string buttonY = $"Joystick{joystickNumber}ButtonY";
        string buttonA = $"Joystick{joystickNumber}ButtonA";
        string buttonB = $"Joystick{joystickNumber}ButtonB";

        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        // Determinar si el personaje esta caminando hacia adelante o hacia atras
        animator.SetBool("IsWalkingForward", horizontal > 0.5f);
        animator.SetBool("IsWalkingBackward", horizontal < -0.5);

        // Esquivas
        if (!isOnAnimation)
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
