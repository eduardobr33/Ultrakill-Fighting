using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;       // Referencia al slider de la barra de vida
    public TextMeshProUGUI healthNum; // Referencia al texto que muestra la vida restante

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthText(maxHealth); // Actualizar el texto al inicializar
    }

    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
        UpdateHealthText(currentHealth); // Actualizar el texto al cambiar la salud
    }

    private void UpdateHealthText(int currentHealth)
    {
        if (healthNum != null)
        {
            healthNum.text = currentHealth.ToString(); // Muestra la salud como texto
        }
    }
}
