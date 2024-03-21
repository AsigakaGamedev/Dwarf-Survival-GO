using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksHandler : MonoBehaviour
{
    [SerializeField] private AAttack[] allAttacks;

    [Space]
    [SerializeField] private float attackDelay;

    private bool canAttack;

    public void OnInitialize()
    {
        canAttack = true;

        foreach (AAttack attack in allAttacks)
        {
            attack.OnInit();
        }
    }

    public bool TryAttack(Vector2 dir)
    {
        if (!canAttack) return false;

        foreach (AAttack attack in allAttacks)
        {
            attack.OnAttack(dir);
        }

        canAttack = false;
        Invoke(nameof(ResetCanAttack), attackDelay);

        return true;
    }

    private void ResetCanAttack()
    {
        canAttack = true;
    }
}
