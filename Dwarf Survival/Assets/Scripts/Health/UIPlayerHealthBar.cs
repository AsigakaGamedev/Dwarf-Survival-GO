using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthBar : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private Slider slider;

    private HealthController health;

    public void OnInitialize()
    {
        health = PlayerManager.Instance.PlayerInstance.Actor.Health;

        OnMaxHealthChange(health.MaxHealth);
        OnHealthChange(health.CurHealth);

        health.onHealthChange += OnHealthChange;
        health.onMaxHealthChange += OnMaxHealthChange;
    }

    public void OnDeinitialize()
    {
        health.onHealthChange -= OnHealthChange;
        health.onMaxHealthChange -= OnMaxHealthChange;
    }

    private void OnHealthChange(float health)
    {
        slider.value = health;
    }

    private void OnMaxHealthChange(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }
}
