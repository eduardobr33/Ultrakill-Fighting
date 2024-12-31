using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI

public class ShootAttack : MonoBehaviour
{
    public Collider attackCollider; // El collider que será usado como ataque
    private int damage = 8; // El daño que inflige el ataque

    private bool canDamage = true;   // Bandera para permitir el daño una sola vez
    private int currentAmmo = 3;     // Munición inicial
    private int maxAmmo = 3;         // Munición máxima

    [Header("UI References")]
    public Slider[] ammoSliders; // Arreglo de Sliders para cada bala

    private float ammoRegenRate = 5f; // Tiempo en segundos para regenerar una bala
    private float smoothSpeed = 1f; // Velocidad de la transición

    private void Start()
    {
        // Asegurarnos de que el collider de ataque esté desactivado al inicio
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
                ammoSliders[i].value = 1;   // Inicialmente están llenos
            }
        }
        else
        {
            Debug.LogError("La cantidad de Sliders no coincide con la munición máxima.");
        }

        // Iniciar regeneración de munición
        StartCoroutine(RegenerateAmmo());
    }

    // Esta función se llamará desde el Animation Event para activar el collider
    public void ActivateAttack()
    {
        // Solo activamos el ataque si hay munición disponible
        if (currentAmmo > 0)
        {
            // Disparar
            canDamage = true;
            attackCollider.enabled = true;

            // Restar una bala y actualizar la UI
            currentAmmo--;
            UpdateAmmoUI();
            Debug.Log("Disparo realizado. Munición restante: " + currentAmmo);
        }
    }

    // Esta función se llamará desde el Animation Event para desactivar el collider
    public void DeactivateAttack()
    {
        if (attackCollider != null)
        {
            canDamage = false;
            attackCollider.enabled = false; // Desactivamos el collider de ataque al final de la animación
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    // Método para actualizar la UI de los Sliders de munición
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
        // Buscar el objeto raíz del collider golpeado
        GameObject hitObject = other.gameObject;

        // Verificar si el objeto raíz no es el mismo que el objeto que ejecuta el ataque
        if (hitObject == gameObject)
        {
            return; // No aplicamos daño a nosotros mismos
        }

        // Verificar si el daño puede aplicarse
        if (canDamage)
        {
            // Buscar el componente PlayerHealth en el padre o en el objeto raíz
            PlayerHealth targetHealth = hitObject.GetComponentInParent<PlayerHealth>();
            if (targetHealth != null)
            {
                if (canDamage) canDamage = false;
                // Aplicar daño al jugador enemigo
                targetHealth.TakeDamage(damage);
                Debug.Log($"{gameObject.name} golpeó a {hitObject.name} con {damage} de daño.");
            }
        }
    }

    // Corrutina para regenerar munición con el tiempo
    private IEnumerator RegenerateAmmo()
    {
        // Aseguramos que la munición no se regenera si ya está llena
        while (true)
        {
            // Esperar el tiempo definido para la regeneración
            
            //yield return new WaitForSeconds(ammoRegenRate);

            // Solo regenerar munición si no está llena
            if (currentAmmo < maxAmmo)
            {
                // Incrementamos la munición de manera suave
                int targetAmmo = currentAmmo + 1; // Objetivo de munición para la regeneración
                StartCoroutine(SmoothAmmoChange(targetAmmo));
            }
        }
    }

    private IEnumerator SmoothAmmoChange(int targetAmmo)
    {
        float currentValue = currentAmmo; // Valor inicial de la munición
        float targetValue = targetAmmo;   // Valor final objetivo de la munición

        // Interpolamos la munición de forma suave
        while (!Mathf.Approximately(currentValue, targetValue))
        {
            currentValue = Mathf.MoveTowards(currentValue, targetValue, smoothSpeed * Time.deltaTime); // Movimiento suave hacia el valor objetivo

            // Actualizamos la UI de las barras de munición
            UpdateAmmoUI(currentValue);

            yield return null; // Esperamos hasta el siguiente frame
        }

        // Aseguramos que la munición esté en el valor final exacto
        currentAmmo = Mathf.RoundToInt(targetValue);
        UpdateAmmoUI(currentAmmo); // Actualizamos la UI de la barra de munición
    }

    private void UpdateAmmoUI(float ammo)
    {
        // Aseguramos que la munición no se salga del rango
        ammo = Mathf.Clamp(ammo, 0, maxAmmo);

        // Calculamos cuántas partes de la barra de munición deben estar llenas
        int filledParts = Mathf.FloorToInt(ammo); // Número de partes llenas (si la munición es 2, dos partes están llenas)

        // Calculamos el porcentaje de la última parte que debe estar llena
        float lastPartFill = ammo - filledParts;

        // Actualizamos las barras de munición de acuerdo al valor actual
        for (int i = 0; i < ammoSliders.Length; i++)
        {
            if (i < filledParts) // Las barras completas
            {
                ammoSliders[i].value = 1f; // Llenamos la barra completamente
            }
            else if (i == filledParts) // La última barra parcialmente llena
            {
                ammoSliders[i].value = lastPartFill; // Llenamos parcialmente la última barra
            }
            else // Las barras vacías
            {
                ammoSliders[i].value = 0f; // Las barras vacías siguen en 0
            }
        }
    }
}
