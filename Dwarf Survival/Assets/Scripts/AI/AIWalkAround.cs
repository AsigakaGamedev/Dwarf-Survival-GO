using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWalkAround : AIActorState
{
    [SerializeField] private float walkRadius;
    [SerializeField] private Vector2 waitTime;

    [Space]
    [ReadOnly, SerializeField] private float currentWaitTime;

    private WorldManager worldManager;

    public override void OnInit(Actor actor)
    {
        base.OnInit(actor);

        worldManager = WorldManager.Instance;
    }

    public override void OnEnterState()
    {
        MoveToRandom();
    }

    public override void OnUpdateState()
    {
        currentWaitTime -= Time.deltaTime;

        if (currentWaitTime <= 0)
        {
            MoveToRandom();
        }
    }

    private void MoveToRandom()
    {
        currentWaitTime = Random.Range(waitTime.x, waitTime.y);
        Vector2 randomPointInRange = (Vector2)transform.position + new Vector2(Random.Range(-walkRadius, walkRadius), Random.Range(-walkRadius, walkRadius));
        try
        {
            WorldCellData randomCell = worldManager.GetCell(randomPointInRange);
            
            if (randomCell.CellType == WorldCellType.Ground)
            {
                actor.MoveAgent(new Vector2(randomCell.PosX, randomCell.PosY));
            }
        }
        catch { }
    }
}
