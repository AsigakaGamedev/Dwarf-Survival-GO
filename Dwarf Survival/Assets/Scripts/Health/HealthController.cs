using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [Space]
    [ReadOnly, SerializeField] private float curHealth;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        curHealth = maxHealth;
    }

    public void Damage(float damage)
    {
        curHealth -= damage;

        if (curHealth < 0)
        {
            curHealth = 0;
        }
    }
}
