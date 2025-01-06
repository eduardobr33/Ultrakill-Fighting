using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAttack : MonoBehaviour
{
    public Collider attackCollider; // El collider que ser� usado como ataque
    private int damage = 3; // El da�o que inflige el ataque

    private bool canDamage = true;   // Bandera para permitir el da�o una sola vez

    private PlayerAudio playerAudio;

    private void Start()
    {
        // Buscar PlayerAudio din�micamente en el padre si no est� asignado
        if (playerAudio == null)
        {
            playerAudio = GetComponentInParent<PlayerAudio>();
            if (playerAudio == null)
            {
                Debug.LogError("PlayerAudio no encontrado en el padre. Aseg�rate de que exista un componente PlayerAudio en la jerarqu�a.");
            }
        }

        // Asegurarnos de que el collider de ataque est� desactivado al inicio
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Desactivamos el collider de ataque al inicio
        }
    }

    // Esta funci�n se llamar� desde el Animation Event para activar el collider
    public void ActivateAttack()
    {
        //Debug.Log("ActivateAttack() fue llamado.");
        if (attackCollider != null)
        {
            canDamage = true;
            attackCollider.enabled = true;
        }

        playerAudio.PlayKick();
    }

    // Esta funci�n se llamar� desde el Animation Event para desactivar el collider
    public void DeactivateAttack()
    {
        if (attackCollider != null)
        {
            canDamage = false;
            attackCollider.enabled = false; // Desactivamos el collider de ataque al final de la animaci�n
        }
    }

    // Detectar cuando el collider de ataque entra en contacto con otro collider
    private void OnTriggerEnter(Collider other)
    {
        // Buscar el objeto ra�z del collider golpeado
        GameObject hitObject = other.gameObject;

        // Verificar si el objeto ra�z no es el mismo que el objeto que ejecuta el ataque
        if (hitObject == gameObject)
        {
            return; // No aplicamos da�o a nosotros mismos
        }

        // Verificar si el da�o puede aplicarse
        if (canDamage)
        {
            // Buscar el componente PlayerHealth en el padre o en el objeto ra�z
            PlayerHealth targetHealth = hitObject.GetComponentInParent<PlayerHealth>();
            if (targetHealth != null)
            {
                if (canDamage) canDamage = false;
                // Aplicar da�o al jugador enemigo
                targetHealth.TakeDamage(damage);
                Debug.Log($"{gameObject.name} golpe� a {hitObject.name} con {damage} de da�o.");

                // Reproducir la animaci�n de "Hit" en el jugador enemigo
                PlayerAnimator targetAnimator = hitObject.GetComponentInParent<PlayerAnimator>();
                if (targetAnimator != null)
                {
                    targetAnimator.PlayHit();
                    playerAudio.PlayHit();
                }
            }
        }
    }
}
