using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Salud máxima del jugador
    private int currentHealth;

    public HealthBar healthBar; // Referencia al script de la barra de vida
    public GameManager gameManager;
    private PlayerAnimator playerAnimator;
    [HideInInspector] public PlayerAnimator otherPlayerAnimator;

    private float winAnimationDelay = 2.0f; // Tiempo de espera antes de reproducir la animación de victoria


    private void Start()
    {
        // Establece la salud inicial al máximo
        currentHealth = maxHealth;

        // Inicializa la barra de vida
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        gameManager = FindObjectOfType<GameManager>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null) healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        // Aquí puedes manejar la muerte del jugador (destruir, respawn, etc.)
        if (playerAnimator != null) playerAnimator.PlayDie();
        if (otherPlayerAnimator != null) StartCoroutine(PlayWinAnimationAfterDelay());

        // Notificar al GameManager
        if (gameManager != null)
        {
            string winner = gameObject.name == "Player1" ? "Player 2" : "Player 1";
            gameManager.EndRound(winner);
        }
    }

    private IEnumerator PlayWinAnimationAfterDelay()
    {
        yield return new WaitForSeconds(winAnimationDelay);
        otherPlayerAnimator?.PlayWin();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
}
