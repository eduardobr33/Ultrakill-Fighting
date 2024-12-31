using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI

public class ShootAttack : MonoBehaviour
{
    public Collider attackCollider; // El collider que ser� usado como ataque
    private int damage = 8; // El da�o que inflige el ataque

    private bool canDamage = true;   // Bandera para permitir el da�o una sola vez
    private int currentAmmo = 3;     // Munici�n inicial
    private int maxAmmo = 3;         // Munici�n m�xima

    [Header("UI References")]
    public Slider[] ammoSliders; // Arreglo de Sliders para cada bala

    private float ammoRegenRate = 5f; // Tiempo en segundos para regenerar una bala
    private float smoothSpeed = 1f; // Velocidad de la transici�n

    private void Start()
    {
        // Asegurarnos de que el collider de ataque est� desactivado al inicio
        if (attackCollider != null)
        {
            attackCollider.enabled = false; // Desactivamos el collider de ataque al inicio
        }

        // Configurar los Sliders
        if (ammoSliders.Length == maxAmmo)
        {
            for (int i = 0; i < ammoSliders.Length; i++)
            {
                ammoSliders[i].maxValue = 1; // Cada Slider representa una bala
                ammoSliders[i].value = 1;   // Inicialmente est�n llenos
            }
        }
        else
        {
            Debug.LogError("La cantidad de Sliders no coincide con la munici�n m�xima.");
        }

        // Iniciar regeneraci�n de munici�n
        StartCoroutine(RegenerateAmmo());
    }

    // Esta funci�n se llamar� desde el Animation Event para activar el collider
    public void ActivateAttack()
    {
        // Solo activamos el ataque si hay munici�n disponible
        if (currentAmmo > 0)
        {
            // Disparar
            canDamage = true;
            attackCollider.enabled = true;

            // Restar una bala y actualizar la UI
            currentAmmo--;
            UpdateAmmoUI();
            Debug.Log("Disparo realizado. Munici�n restante: " + currentAmmo);
        }
    }

    // Esta funci�n se llamar� desde el Animation Event para desactivar el collider
    public void DeactivateAttack()
    {
        if (attackCollider != null)
        {
            canDamage = false;
            attackCollider.enabled = false; // Desactivamos el collider de ataque al final de la animaci�n
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    // M�todo para actualizar la UI de los Sliders de munici�n
    private void UpdateAmmoUI()
    {
        for (int i = 0; i < ammoSliders.Length; i++)
        {
            if (i < currentAmmo)
            {
                ammoSliders[i].value = 1; // Bala disponible
            }
            else
            {
                ammoSliders[i].value = 0; // Bala gastada
            }
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

        // Verificar si el da�o puede aplicarse
        if (canDamage)
        {
            // Buscar el componente PlayerHealth en el padre o en el objeto ra�z
            PlayerHealth targetHealth = hitObject.GetComponentInParent<PlayerHealth>();
            if (targetHealth != null)
            {
                if (canDamage) canDamage = false;
                // Aplicar da�o al jugador enemigo
                targetHealth.TakeDamage(damage);
                Debug.Log($"{gameObject.name} golpe� a {hitObject.name} con {damage} de da�o.");
            }
        }
    }

    // Corrutina para regenerar munici�n con el tiempo
    private IEnumerator RegenerateAmmo()
    {
        // Aseguramos que la munici�n no se regenera si ya est� llena
        while (true)
        {
            // Esperar el tiempo definido para la regeneraci�n
            
            //yield return new WaitForSeconds(ammoRegenRate);

            // Solo regenerar munici�n si no est� llena
            if (currentAmmo < maxAmmo)
            {
                // Incrementamos la munici�n de manera suave
                int targetAmmo = currentAmmo + 1; // Objetivo de munici�n para la regeneraci�n
                StartCoroutine(SmoothAmmoChange(targetAmmo));
            }
        }
    }

    private IEnumerator SmoothAmmoChange(int targetAmmo)
    {
        float currentValue = currentAmmo; // Valor inicial de la munici�n
        float targetValue = targetAmmo;   // Valor final objetivo de la munici�n

        // Interpolamos la munici�n de forma suave
        while (!Mathf.Approximately(currentValue, targetValue))
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, smoothSpeed * Time.deltaTime); // Movimiento suave hacia el valor objetivo

            // Actualizamos la UI de las barras de munici�n
            UpdateAmmoUI(currentValue);

            yield return null; // Esperamos hasta el siguiente frame
        }

        // Aseguramos que la munici�n est� en el valor final exacto
        currentAmmo = Mathf.RoundToInt(targetValue);
        UpdateAmmoUI(currentAmmo); // Actualizamos la UI de la barra de munici�n
    }

    private void UpdateAmmoUI(float ammo)
    {
        // Aseguramos que la munici�n no se salga del rango
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);

        // Calculamos cu�ntas partes de la barra de munici�n deben estar llenas
        int filledParts = Mathf.FloorToInt(ammo); // N�mero de partes llenas (si la munici�n es 2, dos partes est�n llenas)

        // Calculamos el porcentaje de la �ltima parte que debe estar llena
        float lastPartFill = ammo - filledParts;

        // Actualizamos las barras de munici�n de acuerdo al valor actual
        for (int i = 0; i < ammoSliders.Length; i++)
        {
            if (i < filledParts) // Las barras completas
            {
                ammoSliders[i].value = 1f; // Llenamos la barra completamente
            }
            else if (i == filledParts) // La �ltima barra parcialmente llena
            {
                ammoSliders[i].value = lastPartFill; // Llenamos parcialmente la �ltima barra
            }
            else // Las barras vac�as
            {
                ammoSliders[i].value = 0f; // Las barras vac�as siguen en 0
            }
        }
    }
}
