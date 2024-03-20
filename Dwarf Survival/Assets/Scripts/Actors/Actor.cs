using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private NavMeshAgent agent;

    private void OnValidate()
    {
        if (agent) agent.enabled = false;
    }

    public void OnInitialize()
    {
        agent.updateRotation = false;
    }

    public void OnDeinitialize()
    {

    }

    public void ChangeMovementType(bool isRigid)
    {
        if (isRigid)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        agent.enabled = !isRigid;
    }

    public void MoveByDir(Vector2 dir)
    {
        rb.velocity = dir * 3;
    }

    public void MoveAgent(Vector2 targetPoint)
    {
        agent.SetDestination(targetPoint);
    }
}
