using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1Prefab;        // Prefab del jugador
    public GameObject player2Prefab;
    public Transform spawn1;                // Punto de spawn
    public Transform spawn2;

    public HealthBar healthBarPlayer1;      // Barra de vida del jugador
    public HealthBar healthBarPlayer2;

    public Slider[] ammoSlidersPlayer1;     // Sliders de munición del jugador 1
    public Slider[] ammoSlidersPlayer2;     // Sliders de munición del jugador 2

    public GameObject player1;             // Referencia al jugador
    public GameObject player2;

    private int player1Joystick = 0;        // Joystick asignado al jugador
    private int player2Joystick = 0;

    void Update()
    {
        // Detectar si hay un input del mando y asignar jugador 1
        if (player1 == null && DetectInputForPlayer(1))
        {
            SpawnPlayer(1, spawn1, player1Prefab, healthBarPlayer1, ammoSlidersPlayer1);
        }

        // Detectar si hay un input del mando y asignar jugador 2, solo si el jugador 1 ya está asignado
        else if (player1 != null && player2 == null && DetectInputForPlayer(2))
        {
            SpawnPlayer(2, spawn2, player2Prefab, healthBarPlayer2, ammoSlidersPlayer2);
        }
    }

    bool DetectInputForPlayer(int playerNumber)
    {
        string[] joystickNames = Input.GetJoystickNames();

        for (int joystick = 1; joystick <= joystickNames.Length; joystick++)
        {
            if (joystick == player1Joystick || joystick == player2Joystick)
                continue;

            if (Input.GetAxis($"Joystick{joystick}ButtonX") != 0 || Input.anyKey)
            {
                if (playerNumber == 1 && player1 == null)
                {
                    player1Joystick = joystick;
                }
                else if (playerNumber == 2 && player2 == null)
                {
                    player2Joystick = joystick;
                }
                return true;
            }
        }
        return false;
    }

    void SpawnPlayer(int playerNumber, Transform spawnPoint, GameObject prefab, HealthBar healthBar, Slider[] ammoSliders)
    {
        // Instanciar el prefab del jugador en el punto de spawn
        GameObject newPlayer = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        // Asignar el joystick al jugador recién creado
        PlayerInput playerInput = newPlayer.GetComponent<PlayerInput>();

        // Asignar la UI
        PlayerHealth playerHealth = newPlayer.GetComponent<PlayerHealth>();
        ShootAttack shootAttack = newPlayer.GetComponentInChildren<ShootAttack>();
        PlayerAnimator playerAnimator = newPlayer.GetComponent<PlayerAnimator>();

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

        // Asignar las barras de munición al jugador
        if (shootAttack != null && ammoSliders.Length > 0)
        {
            shootAttack.ammoSliders = ammoSliders;
        }

        // Configurar referencias cruzadas entre jugadores
        SetupPlayerReferences();

        Debug.Log($"Player {playerNumber} spawneado con el mando {playerNumber}");
    }

    public void ResetPlayers()
    {
        if (player1 != null)
        {
            PlayerHealth health1 = player1.GetComponent<PlayerHealth>();
            if (health1 != null) health1.ResetHealth();

            PlayerAnimator animator1 = player1.GetComponent<PlayerAnimator>();
            if (animator1 != null) animator1.SetIdle();

            player1.transform.position = spawn1.position;
            player1.transform.rotation = spawn1.rotation;
        }

        if (player2 != null)
        {
            PlayerHealth health2 = player2.GetComponent<PlayerHealth>();
            if (health2 != null) health2.ResetHealth();

            PlayerAnimator animator2 = player2.GetComponent<PlayerAnimator>();
            if (animator2 != null) animator2.SetIdle();

            player2.transform.position = spawn2.position;
            player2.transform.rotation = spawn2.rotation;
        }
    }

    void SetupPlayerReferences()
    {
        if (player1 != null && player2 != null)
        {
            PlayerHealth player1Health = player1.GetComponent<PlayerHealth>();
            PlayerHealth player2Health = player2.GetComponent<PlayerHealth>();

            PlayerInput player1Input = player1.GetComponent<PlayerInput>();
            PlayerInput player2Input = player2.GetComponent<PlayerInput>();

            PlayerAnimator player1Animator = player1.GetComponent<PlayerAnimator>();
            PlayerAnimator player2Animator = player2.GetComponent<PlayerAnimator>();

            // Configurar referencias cruzadas
            if (player1Health != null) player1Health.otherPlayerAnimator = player2Animator;
            if (player2Health != null) player2Health.otherPlayerAnimator = player1Animator;

            if (player1Input != null) player1Input.otherPlayer = player2.transform;
            if (player2Input != null) player2Input.otherPlayer = player1.transform;
        }
    }
}
