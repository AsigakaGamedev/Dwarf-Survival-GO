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

    [Header("Corpse")]
    [SerializeField] private PoolableObject corpsePrefab; 

    public Action<float> onMaxHealthChange;
    public Action<float> onHealthChange;
    public Action onDie;

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
            Kill();
        }

        onHealthChange?.Invoke(maxHealth);
    }

    [Button("Kill", EButtonEnableMode.Playmode)]
    public void Kill()
    {
        curHealth = 0;
        onHealthChange?.Invoke(maxHealth);
        onDie?.Invoke();

        if (corpsePrefab)
        {
            PoolableObject corpse = ServiceLocator.GetService<ObjectPoolingManager>().GetPoolable(corpsePrefab);
            corpse.transform.position = transform.position;
        }
    }
}
