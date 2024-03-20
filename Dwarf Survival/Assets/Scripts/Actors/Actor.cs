using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacteristicsController))]
public class Actor : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private NavMeshAgent agent;

    [Space]
    [SerializeField] private CharacteristicsController characteristics;
    [SerializeField] private ActorInventory inventory;
    [SerializeField] private InteractionsController interactions;

    public ActorInventory Inventory { get => inventory; }
    public InteractionsController Interactions { get => interactions; }

    private void OnValidate()
    {
        if (agent) agent.enabled = false;

        if (!characteristics) characteristics = GetComponent<CharacteristicsController>();

        if (!inventory) inventory = GetComponent<ActorInventory>();

        if (inventory)
        {
            characteristics.AddCharacteristic("cells_count", new CharacteristicEntity(6, 6));
        }
    }

    public void OnInitialize()
    {
        agent.updateRotation = false;

        if (inventory)
        {
            inventory.OnInitialize();
            inventory.SetCellsCount((int)characteristics["cells_count"].CurValue);
        }
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

    public void TryInteract()
    {
        interactions.TryInteract(this);
    }
}
