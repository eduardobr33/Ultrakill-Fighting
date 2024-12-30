using UnityEngine;

public class Attack : MonoBehaviour
{
    public Collider attackCollider; // El collider que será usado como ataque
    public int damage = 10; // El daño que inflige el ataque

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
        // Verificar si el objeto que entra en contacto está en una capa diferente (por ejemplo, si el jugador 1 golpea al jugador 2)
        if (other.gameObject.layer == LayerMask.NameToLayer("Player2"))
        {
            // Aquí puedes aplicar el daño al jugador 2
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("Player 1 golpeó a Player 2 con " + damage + " de daño.");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player1"))
        {
            // Aquí puedes aplicar el daño al jugador 1
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("Player 2 golpeó a Player 1 con " + damage + " de daño.");
        }
    }
}
