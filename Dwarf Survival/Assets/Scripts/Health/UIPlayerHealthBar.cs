using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIPlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private PlayerManager playerManager;
    private HealthController health;

    [Inject]
    public void Construct(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;
    }

    private void OnDestroy()
    {
        if (playerManager)
        {
            playerManager.onPlayerSpawn -= OnPlayerSpawn;
        }

        if (health)
        {
            health.onHealthChange -= OnHealthChange;
            health.onMaxHealthChange -= OnMaxHealthChange;
        }
    }

    private void OnPlayerSpawn(PlayerActorController playerActor)
    {
        if (health)
        {
            health.onHealthChange -= OnHealthChange;
            health.onMaxHealthChange -= OnMaxHealthChange;
        }

        health = playerActor.Actor.Health;

        OnMaxHealthChange(health.MaxHealth);
        OnHealthChange(health.CurHealth);

        health.onHealthChange += OnHealthChange;
        health.onMaxHealthChange += OnMaxHealthChange;
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
