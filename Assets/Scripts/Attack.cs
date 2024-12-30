using UnityEngine;

public class Attack : MonoBehaviour
{
    public Collider attackCollider; // El collider que ser� usado como ataque
    public int damage = 10; // El da�o que inflige el ataque

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
        // Verificar si el objeto que entra en contacto est� en una capa diferente (por ejemplo, si el jugador 1 golpea al jugador 2)
        if (other.gameObject.layer == LayerMask.NameToLayer("Player2"))
        {
            // Aqu� puedes aplicar el da�o al jugador 2
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("Player 1 golpe� a Player 2 con " + damage + " de da�o.");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Player1"))
        {
            // Aqu� puedes aplicar el da�o al jugador 1
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("Player 2 golpe� a Player 1 con " + damage + " de da�o.");
        }
    }
}
