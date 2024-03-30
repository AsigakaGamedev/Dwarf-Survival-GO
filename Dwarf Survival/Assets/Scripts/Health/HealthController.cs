using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [Space]
    [ReadOnly, SerializeField] private float curHealth;

    [Header("Corpse")]
    [SerializeField] private PoolableObject corpsePrefab;

    [Header("Death Drop")]
    [SerializeField] private bool hasDeathDrop;
    [ShowIf(nameof(hasDeathDrop)), SerializeField] private ObjectSpawnData[] droppableObjects;

    private ObjectPoolingManager poolingManager;
    private PoolableObject corpseInstance;

    private bool isDead;

    public Action<float> onMaxHealthChange;
    public Action<float> onHealthChange;
    public Action<BuffData> onHealthChangeBuff;
    public Action onDie;

    public float MaxHealth { get => maxHealth; }
    public float CurHealth { get => curHealth; }

    [Inject]
    private void Construct(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        curHealth = maxHealth;
        onMaxHealthChange?.Invoke(maxHealth);
        onHealthChange?.Invoke(curHealth);
    }

    public void Damage(float damage)
    {
        curHealth -= damage;
        onHealthChange?.Invoke(curHealth);

        if (curHealth <= 0)
        {
            curHealth = 0;
            Kill();
        }
    }

    public void Damage(float damage, BuffData buff)
    {
        onHealthChangeBuff?.Invoke(buff);
        Damage(damage);
    }

    [Button("Kill", EButtonEnableMode.Playmode)]
    public void Kill()
    {
        if (isDead) return;

        isDead = true;

        curHealth = 0;
        onHealthChange?.Invoke(curHealth);
        onDie?.Invoke();

        if (corpsePrefab)
        {
            corpseInstance = poolingManager.GetPoolable(corpsePrefab);
            corpseInstance.transform.position = transform.position;
        }

        if (hasDeathDrop)
        {
            foreach (ObjectSpawnData deathDrop in droppableObjects)
            {
                if (!deathDrop.CanSpawn()) continue;

                for (int i = 0; i < deathDrop.SpawnAmount; i++)
                {
                    PoolableObject newDroppedObj = poolingManager.GetPoolable(deathDrop.Prefab);
                    newDroppedObj.transform.position = transform.position;
                }
            }
        }
    }

    [Button("Revive", EButtonEnableMode.Playmode)]
    public void Revive()
    {
        if (!isDead) return;

        isDead = false;

        curHealth = maxHealth;
        onHealthChange?.Invoke(curHealth);

        if (corpseInstance)
        {
            corpseInstance.gameObject.SetActive(false);
        }
    }
}
