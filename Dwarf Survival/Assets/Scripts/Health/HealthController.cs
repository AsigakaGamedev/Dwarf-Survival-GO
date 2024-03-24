using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [Space]
    [ReadOnly, SerializeField] private float curHealth;

    public Action<float> onMaxHealthChange;
    public Action<float> onHealthChange;

    public float MaxHealth { get => maxHealth; }
    public float CurHealth { get => curHealth; }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        curHealth = maxHealth;
        onMaxHealthChange?.Invoke(maxHealth);
    }

    public void Damage(float damage)
    {
        curHealth -= damage;

        if (curHealth < 0)
        {
            curHealth = 0;
        }

        onHealthChange?.Invoke(maxHealth);
    }
}
