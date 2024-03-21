using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AAttack
{
    [SerializeField] private LayerMask damagableLayers;
    [SerializeField] private float damageRadius;
    [SerializeField] private float damageValue;

    public override void OnAttack(Vector2 dir)
    {
        print("Melee Attack");
    }
}
