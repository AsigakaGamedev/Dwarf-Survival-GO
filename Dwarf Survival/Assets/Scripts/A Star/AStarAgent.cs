using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum AStarAgentState { Idle, Move, Stopped}

public class AStarAgent : MonoBehaviour
{
    private float stoppingDistance = 0.75f;

    [Space]
    [ReadOnly, SerializeField] private AStarAgentState state;
    [ReadOnly, SerializeField] private List<Vector2> path;

    private AStarPathfinding pathfinding;

    private float moveSpeed;

    private int curPathIndex;

    public bool IsStopped { 
        set
        {
            if (value == true)
            {
                state = AStarAgentState.Stopped;
            }
            else if (path.Count > 0)
            {
                state = AStarAgentState.Move;
            }
            else
            {
                state = AStarAgentState.Idle;
            }
        } 
    }

    [Inject]
    private void Construct(AStarPathfinding pathfinding)
    {
        this.pathfinding = pathfinding;
    }

    private void Update()
    {
        if (state == AStarAgentState.Idle || state == AStarAgentState.Stopped) return;

        if (curPathIndex >= path.Count)
        {
            state = AStarAgentState.Idle;
            return;
        }

        if (Vector2.Distance(transform.position, path[curPathIndex]) <= stoppingDistance)
        {
            curPathIndex++;
            return;
        }

        Vector2 moveDir = path[curPathIndex] - (Vector2)transform.position;
        moveDir.Normalize();

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    public void SetDistanation(Vector2 targetPoint)
    {
        path = pathfinding.FindPath((Vector2)transform.position, targetPoint);

        if (path.Count > 0)
        {
            state = AStarAgentState.Move;
            curPathIndex = 0;
        }
        else
        {
            state = AStarAgentState.Idle;
        }
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
