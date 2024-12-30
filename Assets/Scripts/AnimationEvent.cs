using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationEventSearcher : MonoBehaviour
{
    private PunchAttack punchAttack;
    private KickAttack kickAttack;
    private ShootAttack shootAttack;

    private void Start()
    {
        // Busca autom�ticamente el script Attack en los hijos
        punchAttack = GetComponentInChildren<PunchAttack>();
        kickAttack = GetComponentInChildren<KickAttack>();
        shootAttack = GetComponentInChildren<ShootAttack>();

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
            shootAttack.ActivateAttack();
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
}
