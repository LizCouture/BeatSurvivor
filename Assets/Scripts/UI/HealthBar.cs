using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Start() {
        Player.Instance.playerHealthChanged.AddListener(setHealth);
        Player.Instance.playerHealthMaxChanged.AddListener(setMaxHealth);
    }

    private void setMaxHealth() {
        Debug.Log("setMaxHealth");
        slider.maxValue = Player.Instance.MaxHealth;
        // TODO: Do we want this to happen?
        slider.value = slider.maxValue;
    }

    private void setHealth() {
        setMaxHealth();
        Debug.Log("setHealth to: " + Player.Instance.CurrentHealth);
        slider.value = Player.Instance.CurrentHealth;
        Debug.Log("slider value = " + slider.value + " out of " + slider.maxValue);
    }
}
