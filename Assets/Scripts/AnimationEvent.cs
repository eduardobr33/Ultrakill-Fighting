using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationEventSearcher : MonoBehaviour
{
    private PunchAttack punchAttack;
    private KickAttack kickAttack;
    private ShootAttack shootAttack;
    private DodgeHigh dodgeHigh;
    private DodgeLow dodgeLow;

    private Camera mainCamera;
    public Camera playerCamera;

    private void Start()
    {
        // Busca automáticamente el script Attack en los hijos
        punchAttack = GetComponentInChildren<PunchAttack>();
        kickAttack = GetComponentInChildren<KickAttack>();
        shootAttack = GetComponentInChildren<ShootAttack>();
        dodgeHigh = GetComponentInChildren<DodgeHigh>();
        dodgeLow = GetComponentInChildren<DodgeLow>();

        mainCamera = Camera.main;

        if (punchAttack == null)
        {
            Debug.LogError("No se encontró un script PunchAttack en los hijos.");
        }
        else if (kickAttack == null)
        {
            Debug.LogError("No se encontró un script KickAttack en los hijos.");
        }
        else if (shootAttack == null)
        {
            Debug.LogError("No se encontró un script ShootAttack en los hijos.");
        }
        else if (dodgeHigh == null)
        {
            Debug.LogError("No se encontró un script DodgeHigh en los hijos.");
        }
        else if (dodgeLow == null)
        {
            Debug.LogError("No se encontró un script DodgeLow en los hijos.");
        }
    }

    public void ActivatePunchAttack()
    {
        if (punchAttack != null)
        {
            punchAttack.ActivateAttack();
        }
        else Debug.LogError("El script PunchAttack no está asignado.");
    }

    public void DeactivatePunchAttack()
    {
        if(punchAttack != null)
        {
            punchAttack.DeactivateAttack();
        }
        else Debug.LogError("El script PunchAttack no está asignado.");
    }



    public void ActivateKickAttack()
    {
        if (kickAttack != null)
        {
            kickAttack.ActivateAttack();
        }
        else Debug.LogError("El script KickAttack no está asignado.");
    }

    public void DeactivateKickAttack()
    {
        if (kickAttack != null)
        {
            kickAttack.DeactivateAttack();
        }
        else Debug.LogError("El script KickAttack no está asignado.");
    }



    public void ActivateShootAttack()
    {
        if (shootAttack != null)
        {
            // Verificamos si hay munición antes de activar el ataque
            if (shootAttack.GetCurrentAmmo() > 0)
            {
                shootAttack.ActivateAttack();
            }
            else Debug.Log("No hay munición suficiente para disparar.");
        }
        else Debug.LogError("El script ShootAttack no está asignado.");
    }

    public void DeactivateShootAttack()
    {
        if (shootAttack != null)
        {
            shootAttack.DeactivateAttack();
        }
        else Debug.LogError("El script ShootAttack no está asignado.");
    }



    public void ActivateDodgeHigh()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.ActivateDodge();
        }
        else Debug.LogError("El script DodgeHigh no está asignado.");
    }

    public void DeactivateDodgeHigh()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.DeactivateDodge();
        }
        else Debug.LogError("El script DodgeHigh no está asignado.");
    }



    public void ActivateDodgeLow()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.ActivateDodge();
        }
        else Debug.LogError("El script DodgeLow no está asignado.");
    }

    public void DeactivateDodgeLow()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.DeactivateDodge();
        }
        else Debug.LogError("El script DodgeLow no está asignado.");
    }



    public void ActivateCamera()
    {
        if (playerCamera != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            Debug.Log("Cámara activada.");
        }
        else Debug.LogError("No se encontró una cámara para activar.");
    }

    public void DeactivateCamera()
    {
        if (playerCamera != null && mainCamera != null)
        {
            playerCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            Debug.Log("Cámara desactivada.");
        }
        else Debug.LogError("No se encontró una cámara para desactivar.");
    }
}
