using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider slider;

    private void Update() {
        if(slider.value <= 0) {
            slider.gameObject.SetActive(false);
        }
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health) {
        slider.value = health;
    }
}
