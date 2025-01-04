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
        // Busca autom�ticamente el script Attack en los hijos
        punchAttack = GetComponentInChildren<PunchAttack>();
        kickAttack = GetComponentInChildren<KickAttack>();
        shootAttack = GetComponentInChildren<ShootAttack>();
        dodgeHigh = GetComponentInChildren<DodgeHigh>();
        dodgeLow = GetComponentInChildren<DodgeLow>();

        mainCamera = Camera.main;

        if (punchAttack == null)
        {
            Debug.LogError("No se encontr� un script PunchAttack en los hijos.");
        }
        else if (kickAttack == null)
        {
            Debug.LogError("No se encontr� un script KickAttack en los hijos.");
        }
        else if (shootAttack == null)
        {
            Debug.LogError("No se encontr� un script ShootAttack en los hijos.");
        }
        else if (dodgeHigh == null)
        {
            Debug.LogError("No se encontr� un script DodgeHigh en los hijos.");
        }
        else if (dodgeLow == null)
        {
            Debug.LogError("No se encontr� un script DodgeLow en los hijos.");
        }
    }

    public void ActivatePunchAttack()
    {
        if (punchAttack != null)
        {
            punchAttack.ActivateAttack();
        }
        else Debug.LogError("El script PunchAttack no est� asignado.");
    }

    public void DeactivatePunchAttack()
    {
        if(punchAttack != null)
        {
            punchAttack.DeactivateAttack();
        }
        else Debug.LogError("El script PunchAttack no est� asignado.");
    }



    public void ActivateKickAttack()
    {
        if (kickAttack != null)
        {
            kickAttack.ActivateAttack();
        }
        else Debug.LogError("El script KickAttack no est� asignado.");
    }

    public void DeactivateKickAttack()
    {
        if (kickAttack != null)
        {
            kickAttack.DeactivateAttack();
        }
        else Debug.LogError("El script KickAttack no est� asignado.");
    }



    public void ActivateShootAttack()
    {
        if (shootAttack != null)
        {
            // Verificamos si hay munici�n antes de activar el ataque
            if (shootAttack.GetCurrentAmmo() > 0)
            {
                shootAttack.ActivateAttack();
            }
            else Debug.Log("No hay munici�n suficiente para disparar.");
        }
        else Debug.LogError("El script ShootAttack no est� asignado.");
    }

    public void DeactivateShootAttack()
    {
        if (shootAttack != null)
        {
            shootAttack.DeactivateAttack();
        }
        else Debug.LogError("El script ShootAttack no est� asignado.");
    }



    public void ActivateDodgeHigh()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.ActivateDodge();
        }
        else Debug.LogError("El script DodgeHigh no est� asignado.");
    }

    public void DeactivateDodgeHigh()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.DeactivateDodge();
        }
        else Debug.LogError("El script DodgeHigh no est� asignado.");
    }



    public void ActivateDodgeLow()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.ActivateDodge();
        }
        else Debug.LogError("El script DodgeLow no est� asignado.");
    }

    public void DeactivateDodgeLow()
    {
        if (dodgeHigh != null)
        {
            dodgeHigh.DeactivateDodge();
        }
        else Debug.LogError("El script DodgeLow no est� asignado.");
    }



    public void ActivateCamera()
    {
        if (playerCamera != null && mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            Debug.Log("C�mara activada.");
        }
        else Debug.LogError("No se encontr� una c�mara para activar.");
    }

    public void DeactivateCamera()
    {
        if (playerCamera != null && mainCamera != null)
        {
            playerCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
            Debug.Log("C�mara desactivada.");
        }
        else Debug.LogError("No se encontr� una c�mara para desactivar.");
    }
}
