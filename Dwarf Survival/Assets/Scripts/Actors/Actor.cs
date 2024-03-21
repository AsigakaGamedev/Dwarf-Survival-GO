using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacteristicsController))]
public class Actor : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private string id;

    [Space]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private NavMeshAgent agent;

    [Space]
    [SerializeField] private CharacteristicsController characteristics;
    [SerializeField] private HealthController health;
    [SerializeField] private ActorInventory inventory;
    [SerializeField] private InteractionsController interactions;
    [SerializeField] private ActorVision vision;
    [SerializeField] private EquipmentsController equipments;
    [SerializeField] private WeaponsController weapons;

    [Space]
    [SerializeField] private List<string> enemiesID;

    public string ID { get => id; }

    public ActorInventory Inventory { get => inventory; }
    public InteractionsController Interactions { get => interactions; }
    public ActorVision Vision { get => vision; }
    public EquipmentsController Equipments { get => equipments; }
    public WeaponsController Weapons { get => weapons; }

    private void OnValidate()
    {
        if (agent) agent.enabled = false;

        if (!characteristics) characteristics = GetComponent<CharacteristicsController>();

        if (!inventory) inventory = GetComponent<ActorInventory>();

        if (inventory)
        {
            characteristics.AddCharacteristic("cells_count", new CharacteristicEntity(6));
        }

        if (!health) inventory = GetComponent<ActorInventory>();

        if (health)
        {
            characteristics.AddCharacteristic("max_health", new CharacteristicEntity(100));
            health.SetMaxHealth(characteristics["max_health"].Value);
        }
    }

    public void OnInitialize()
    {
        agent.updateRotation = false;

        if (inventory)
        {
            inventory.OnInitialize();
            inventory.SetCellsCount((int)characteristics["cells_count"].Value);
        }

        if (vision)
        {
            vision.Actor = this;
            vision.OnInitialize();
        }

        if (weapons)
        {
            weapons.OnInitialize();
        }

        if (health)
        {
            health.SetMaxHealth(characteristics["max_health"].Value);
        }
    }

    public void OnDeinitialize()
    {
        if (vision)
        {
            vision.OnDeinitialize();
        }
    }

    #region Movement

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

    #endregion

    #region Combat 

    public bool IsEnemy(string actorID)
    {
        return enemiesID.Contains(actorID);
    }

    public void AddEnemy(string newEnemy)
    {
        if (!enemiesID.Contains(newEnemy))
        {
            enemiesID.Add(newEnemy);
        }
    }

    public void Attack(Vector2 dir)
    {
        if (weapons) weapons.TryAttack(dir);
    }

    #endregion

    public void TryInteract()
    {
        interactions.TryInteract(this);
    }
}
