using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1Prefab;        // Prefab del jugador
    public GameObject player2Prefab;
    public Transform spawn1;                // Punto de spawn
    public Transform spawn2;

    public HealthBar healthBarPlayer1;      // Barra de vida del jugador
    public HealthBar healthBarPlayer2;

    private GameObject player1;             // Referencia al jugador
    private GameObject player2;

    private int player1Joystick = 0;        // Joystick asignado al jugador
    private int player2Joystick = 0;

    void Update()
    {
        // Detectar si hay un input del mando y asignar jugador 1
        if (player1 == null && DetectInputForPlayer(1))
        {
            SpawnPlayer(1, spawn1, player1Prefab, healthBarPlayer1);
        }

        // Detectar si hay un input del mando y asignar jugador 2, solo si el jugador 1 ya está asignado
        if (player1 != null && player2 == null && DetectInputForPlayer(2))
        {
            SpawnPlayer(2, spawn2, player2Prefab, healthBarPlayer2);
        }

        //// Si no se detecta un segundo mando, pero ya hay un jugador 1, asignar el segundo jugador al primer mando.
        //if (player1 != null && player2 == null && DetectInputForPlayer(1))
        //{
        //    SpawnPlayer(2, spawn2, player2Prefab);
        //}
    }

    bool DetectInputForPlayer(int playerNumber)
    {
        // Detectar si hay entrada de joystick o teclado
        for (int joystick = 1; joystick <= 2; joystick++) // Soporte hasta 2 mandos
        {
            // Evitar reasignar un mando ya asignado
            if (joystick == player1Joystick || joystick == player2Joystick)
                continue;

            // Detectar si se ha presionado un botón en el mando
            if (Input.GetButton($"Joystick{joystick}ButtonA") || Input.GetButton($"Joystick{joystick}ButtonB") 
                || Input.GetButton($"Joystick{joystick}ButtonX") || Input.GetButton($"Joystick{joystick}ButtonY"))
            {
                if (playerNumber == 1 && player1 == null) // Solo asignar el mando al primer jugador si no hay uno ya asignado
                {
                    player1Joystick = joystick;
                }
                else if (playerNumber == 2 && player2 == null) // Solo asignar el mando al segundo jugador si no hay uno ya asignado
                {
                    player2Joystick = joystick;
                }
                return true;
            }
        }

        return false;
    }

    void SpawnPlayer(int playerNumber, Transform spawnPoint, GameObject prefab, HealthBar healthBar)
    {
        // Instanciar el prefab del jugador en el punto de spawn
        GameObject newPlayer = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        // Asignar el joystick al jugador recién creado
        PlayerInput playerInput = newPlayer.GetComponent<PlayerInput>();

        PlayerHealth playerHealth = newPlayer.GetComponent<PlayerHealth>();

        if (playerNumber == 1)
        {
            playerInput.SetJoystickNumber(player1Joystick);
            player1 = newPlayer; // Guardar referencia al jugador 1
        }
        else if (playerNumber == 2)
        {
            playerInput.SetJoystickNumber(player2Joystick);
            player2 = newPlayer; // Guardar referencia al jugador 2
        }

        // Asignar la barra de vida al jugador
        if (playerHealth != null)
        {
            playerHealth.healthBar = healthBar;
        }

        Debug.Log($"Player {playerNumber} spawneado con el mando {playerNumber}");
    }
}
