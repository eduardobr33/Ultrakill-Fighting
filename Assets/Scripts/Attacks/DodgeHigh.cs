using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeHigh : MonoBehaviour
{
    public Collider dodgeCollider;

    private PlayerAudio playerAudio;

    private void Start()
    {
        // Buscar PlayerAudio dinámicamente en el padre si no está asignado
        if (playerAudio == null)
        {
            playerAudio = GetComponentInParent<PlayerAudio>();
            if (playerAudio == null)
            {
                Debug.LogError("PlayerAudio no encontrado en el padre. Asegúrate de que exista un componente PlayerAudio en la jerarquía.");
            }
        }
    }

    public void ActivateDodge()
    {
        if (dodgeCollider != null)
        {
            dodgeCollider.enabled = false;
        }

        playerAudio.PlayDodge();
    }

    public void DeactivateDodge()
    {
        if (dodgeCollider != null)
        {
            dodgeCollider.enabled = true;
        }
    }
}