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
}
