using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;       // Referencia al slider de la barra de vida
    public TextMeshProUGUI healthNum; // Referencia al texto que muestra la vida restante
    private float smoothSpeed = 50f; // Velocidad de la transición

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthText(maxHealth); // Actualizar el texto al inicializar
    }

    public void SetHealth(int currentHealth)
    {
        StartCoroutine(SmoothHealthChange(currentHealth));
    }

    private IEnumerator SmoothHealthChange(int targetHealth)
    {
        float currentValue = healthSlider.value; // Valor inicial de la barra
        float targetValue = targetHealth;       // Valor final objetivo

        while (!Mathf.Approximately(currentValue, targetValue)) // Comparación precisa
        {
            // Interpolamos hacia el valor objetivo
            currentValue = Mathf.MoveTowards(currentValue, targetValue, smoothSpeed * Time.deltaTime);
            healthSlider.value = currentValue; // Actualizamos el valor del slider

            yield return null; // Esperamos hasta el siguiente frame
        }

        // Nos aseguramos de que la barra esté en el valor final exacto
        healthSlider.value = targetValue;
        UpdateHealthText(targetHealth); // Actualizar el texto al cambiar la salud
    }

    private void UpdateHealthText(int currentHealth)
    {
        if (healthNum != null)
        {
            healthNum.text = currentHealth.ToString(); // Muestra la salud como texto
        }
    }
}
