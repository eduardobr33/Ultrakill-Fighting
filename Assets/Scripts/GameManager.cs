using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;  // Referencia al PlayerManager
    public GameObject healthPlayer1;         // Contenedor de la UI del jugador 1
    public GameObject ammoPlayer1;
    public GameObject healthPlayer2;         // Contenedor de la UI del jugador 2
    public GameObject ammoPlayer2;
    public TextMeshProUGUI continueText;

    private bool gameStarted = false;    // Estado del juego

    private int roundNumber = 1;         // Número de ronda actual
    private int maxRounds = 3;           // Número máximo de rondas
    public float roundTransitionDelay = 3.0f; // Tiempo de espera entre rondas

    private void Start()
    {
        // Desactivar las UI de los jugadores al inicio
        if (healthPlayer1 != null) healthPlayer1.SetActive(false);
        if (ammoPlayer1 != null) ammoPlayer1.SetActive(false);
        if (healthPlayer2 != null) healthPlayer2.SetActive(false);
        if (ammoPlayer2 != null) ammoPlayer2.SetActive(false);
    }

    private void Update()
    {
        // Iniciar el juego cuando ambos jugadores estén spawneados
        if (!gameStarted && playerManager.player1 != null && playerManager.player2 != null)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        gameStarted = true;

        // Activar la UI de los jugadores
        if (healthPlayer1 != null) healthPlayer1.SetActive(true);
        if (ammoPlayer1 != null) ammoPlayer1.SetActive(true);
        if (healthPlayer2 != null) healthPlayer2.SetActive(true);
        if (ammoPlayer2 != null) ammoPlayer2.SetActive(true);

        playerManager.ResetPlayers();

        Debug.Log($"¡Comienza la ronda {roundNumber}!");
    }

    public void EndRound(string winner)
    {
        Debug.Log($"Ronda terminada. Ganador: {winner}");
        StartCoroutine(HandleEndRound(winner));
    }

    private IEnumerator HandleEndRound(string winner)
    {
        // Esperar unos segundos para reproducir la animación de victoria
        yield return new WaitForSeconds(2.0f);

        if (continueText != null) continueText.text = $"{winner} Wins!";

        // Esperar antes de iniciar la siguiente ronda
        yield return new WaitForSeconds(roundTransitionDelay);

        ResetRound();
    }

    private void ResetRound()
    {
        Debug.Log("Preparando la siguiente ronda...");

        // Resetear a los jugadores (vidas, posiciones, etc.)
        playerManager.ResetPlayers();

        gameStarted = false;

        // Desactivar las UI hasta que los jugadores estén listos
        if (healthPlayer1 != null) healthPlayer1.SetActive(false);
        if (ammoPlayer1 != null) ammoPlayer1.SetActive(false);
        if (healthPlayer2 != null) healthPlayer2.SetActive(false);
        if (ammoPlayer2 != null) ammoPlayer2.SetActive(false);

        roundNumber++;

        // Revisar si el juego ha terminado
        if (roundNumber > maxRounds)
        {
            Debug.Log("¡Juego terminado!");
            // Aquí puedes mostrar una pantalla final o reiniciar el juego
        }
        else
        {
            Debug.Log($"Ronda {roundNumber} lista para comenzar.");
            StartGame();
        }
    }
}
