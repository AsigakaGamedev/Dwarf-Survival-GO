using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AAttack
{
    [SerializeField] private LayerMask damagableLayers;
    [SerializeField] private float damageRadius;
    [SerializeField] private float damageValue;
    [SerializeField] private Collider2D ignoredCollider;

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
            if (col == ignoredCollider) continue;

            if (col.TryGetComponent(out HealthController health))
            {
                health.Damage(damageValue);
            }
        }
    }
}
