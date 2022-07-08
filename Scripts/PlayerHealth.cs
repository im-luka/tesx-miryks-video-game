using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider slider;
    public Text healthText;

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
        healthText.text = (slider.value + " / " + slider.maxValue).ToString();
    }

    public void SetHealth(int health) {
        slider.value = health;
        healthText.text = (slider.value + " / " + slider.maxValue).ToString();
    }
}
