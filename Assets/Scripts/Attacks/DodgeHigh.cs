using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeHigh : MonoBehaviour
{
    public Collider dodgeCollider;

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