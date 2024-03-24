using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPursueAndAttackEnemy : AIActorState
{
    [SerializeField] private float attackDistance = 1.1f;
    [SerializeField] private float waitBeforeAttackDelay = 2;

    private ActorVision vision;

    private Transform target;
    private Vector3 lastTargetPos;

    private bool hasWaitBeforeAttack;

    public override void OnInit(Actor actor)
    {
        base.OnInit(actor);

        vision = actor.Vision;
        hasWaitBeforeAttack = false;
    }

    public override bool CanEnterState()
    {
        if (vision.Enemies.Count > 0)
        {
            Transform newTarget = vision.Enemies[0].transform;
            
            if (!target)
            {
                target = newTarget;
                lastTargetPos = target.position;
                actor.MoveAgent(target.position);
            }

            return true;
        }
        target = null;
        return false;
    }

    public override void OnUpdateState()
    {
        if (hasWaitBeforeAttack) return;

        if (lastTargetPos != target.position)
        {
            actor.MoveAgent(target.position);
            lastTargetPos = target.position;
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDistance)
        {
            actor.ChangeIsStopped(true);
            hasWaitBeforeAttack = true;
            Invoke(nameof(OnWaitBeforeAttackEnd), waitBeforeAttackDelay);
        }
    }

    private void OnWaitBeforeAttackEnd()
    {
        actor.ChangeIsStopped(false);
        hasWaitBeforeAttack = false;
    }
}
