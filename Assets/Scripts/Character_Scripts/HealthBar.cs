using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxHealth(float maxHealth)
    {

        slider.maxValue = maxHealth;
        slider.value = maxHealth;

    }

    public void SetHealth(float currentHealth)
    {



        slider.value = currentHealth;

    }



}
