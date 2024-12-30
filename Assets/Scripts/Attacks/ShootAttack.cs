using UnityEngine;

public class ShootAttack : MonoBehaviour
{
    public Collider attackCollider; // El collider que ser� usado como ataque
    private int damage = 8; // El da�o que inflige el ataque

    private void Start()
    {
        // Asegurarnos de que el collider de ataque est� desactivado al inicio
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Desactivamos el collider de ataque al inicio
        }
    }

    // Esta funci�n se llamar� desde el Animation Event para activar el collider
    public void ActivateAttack()
    {
        Debug.Log("ActivateAttack() fue llamado.");
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }

    // Esta funci�n se llamar� desde el Animation Event para desactivar el collider
    public void DeactivateAttack()
    {
        if (attackCollider != null)
        {
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

        // Buscar el componente PlayerHealth en el padre o en el objeto ra�z
        PlayerHealth targetHealth = hitObject.GetComponentInParent<PlayerHealth>();
        if (targetHealth != null)
        {
            // Aplicar da�o al jugador enemigo
            targetHealth.TakeDamage(damage);
            Debug.Log($"{gameObject.name} golpe� a {hitObject.name} con {damage} de da�o.");
        }
    }
}
