using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AAttack
{
    [SerializeField] private LayerMask damagableLayers;
    [SerializeField] private float damageRadius;
    [SerializeField] private float damageValue;
    [SerializeField] private Collider2D ignoredCollider;

    [Space]
    [SerializeField] private bool hasDamageBuff;
    [ShowIf(nameof(hasDamageBuff)), Expandable, SerializeField] private BuffInfo damageBuffInfo;
    [ShowIf(nameof(canShowBuffData)), SerializeField] private BuffData damageBuffData;

    private bool canShowBuffData => damageBuffInfo == null && hasDamageBuff;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

    public override void OnAttack(Vector2 dir)
    {
        Collider2D[] collidersAround = Physics2D.OverlapCircleAll(transform.position, damageRadius, damagableLayers);

        foreach (Collider2D col in collidersAround)
        {
            if (col == ignoredCollider || !col.TryGetComponent(out HealthController health)) continue;

            if (hasDamageBuff)
            {
                if (damageBuffInfo)
                {
                    health.Damage(damageValue, damageBuffInfo.BuffData);
                }
                else
                {
                    health.Damage(damageValue, damageBuffData);
                }
            }
            else
            {
                health.Damage(damageValue);
            }
        }
    }
}
