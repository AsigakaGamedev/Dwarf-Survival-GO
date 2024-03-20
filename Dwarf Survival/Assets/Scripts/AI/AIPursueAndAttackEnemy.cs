using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPursueAndAttackEnemy : AIActorState
{
    private ActorVision vision;

    public override void OnInit(Actor actor)
    {
        base.OnInit(actor);

        vision = actor.Vision;
    }

    public override bool CanEnterState()
    {
        return vision.Enemies.Count > 0;
    }

    public override void OnUpdateState()
    {
        actor.MoveAgent(vision.Enemies[0].transform.position);   
    }
}
