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
}
