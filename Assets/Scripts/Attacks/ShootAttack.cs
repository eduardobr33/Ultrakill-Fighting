using UnityEngine;

public class ShootAttack : MonoBehaviour
{
    public Collider attackCollider; // El collider que será usado como ataque
    private int damage = 8; // El daño que inflige el ataque

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
        Debug.Log("ActivateAttack() fue llamado.");
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }

    // Esta función se llamará desde el Animation Event para desactivar el collider
    public void DeactivateAttack()
    {
        if (attackCollider != null)
        {
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

        // Buscar el componente PlayerHealth en el padre o en el objeto raíz
        PlayerHealth targetHealth = hitObject.GetComponentInParent<PlayerHealth>();
        if (targetHealth != null)
        {
            // Aplicar daño al jugador enemigo
            targetHealth.TakeDamage(damage);
            Debug.Log($"{gameObject.name} golpeó a {hitObject.name} con {damage} de daño.");
        }
    }
}
