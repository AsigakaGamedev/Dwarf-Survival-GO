using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningObjectsAttack : AAttack
{
    [SerializeField] private LayerMask miningLayers;
    [SerializeField] private float miningRadius = 2;
    [SerializeField] private float miningPower = 5;

    public override void OnAttack(Vector2 dir)
    {
        Collider2D[] collidersAround = Physics2D.OverlapCircleAll(transform.position, miningRadius, miningLayers);

        foreach (Collider2D col in collidersAround)
        {
            if (col.TryGetComponent(out MineableObject mineableObject))
            {
                mineableObject.Mine(miningPower);
            }
        }
    }
}
