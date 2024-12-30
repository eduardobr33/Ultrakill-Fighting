using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del jugador
    private int currentHealth;

    public HealthBar healthBar; // Referencia al script de la barra de vida

    private void Start()
    {
        // Establece la salud inicial al máximo
        currentHealth = maxHealth;

        // Inicializa la barra de vida
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        // Resta la salud actual
        currentHealth -= damage;

        // Actualiza la barra de vida
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        // Si la salud llega a 0, el jugador muere
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        // Aquí puedes manejar la muerte del jugador (destruir, respawn, etc.)
    }
}
