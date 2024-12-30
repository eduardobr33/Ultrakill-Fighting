using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    public Collider attackCollider; // El collider que será usado como ataque
    private int damage = 3; // El daño que inflige el ataque

    private bool canDamage = true;   // Bandera para permitir el daño una sola vez

    private void Start()
    {
        // Asegurarnos de que el collider de ataque esté desactivado al inicio
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Desactivamos el collider de ataque al inicio
        }
    }

    // Esta función se llamará desde el Animation Event para activar el collider
    public void ActivateAttack()
    {
        //Debug.Log("ActivateAttack() fue llamado.");
        if (attackCollider != null)
        {
            canDamage = true;
            attackCollider.enabled = true;
        }
    }

    // Esta función se llamará desde el Animation Event para desactivar el collider
    public void DeactivateAttack()
    {
        if (attackCollider != null)
        {
            canDamage = false;
            attackCollider.enabled = false; // Desactivamos el collider de ataque al final de la animación
        }
    }

    // Detectar cuando el collider de ataque entra en contacto con otro collider
    private void OnTriggerEnter(Collider other)
    {
        // Buscar el objeto raíz del collider golpeado
        GameObject hitObject = other.gameObject;

        // Verificar si el objeto raíz no es el mismo que el objeto que ejecuta el ataque
        if (hitObject == gameObject)
        {
            return; // No aplicamos daño a nosotros mismos
        }

        // Verificar si el daño puede aplicarse
        if (canDamage)
        {
            // Buscar el componente PlayerHealth en el padre o en el objeto raíz
            PlayerHealth targetHealth = hitObject.GetComponentInParent<PlayerHealth>();
            if (targetHealth != null)
            {
                if (canDamage) canDamage = false;
                // Aplicar daño al jugador enemigo
                targetHealth.TakeDamage(damage);
                Debug.Log($"{gameObject.name} golpeó a {hitObject.name} con {damage} de daño.");
            }
        }
    }
}
