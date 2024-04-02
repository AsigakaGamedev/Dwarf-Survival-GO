using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacteristicsController))]
public class Actor : MonoBehaviour
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
    [SerializeField] private NeedsController needs;
    [SerializeField] private BuffsController buffs;

    [Space]
    [SerializeField] private List<string> enemiesID;

    public string ID { get => id; }

    public ActorInventory Inventory { get => inventory; }
    public InteractionsController Interactions { get => interactions; }
    public ActorVision Vision { get => vision; }
    public EquipmentsController Equipments { get => equipments; }
    public WeaponsController Weapons { get => weapons; }
    public NeedsController Needs { get => needs; }
    public HealthController Health { get => health; }
    public BuffsController Buffs { get => buffs; }

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
            inventory.Init();
            inventory.SetCellsCount((int)characteristics["cells_count"].Value);
            inventory.AddStartItems();

            inventory.onItemUse += OnItemUse;
            inventory.onItemEquip += OnItemEquip;
        }

        if (vision)
        {
            vision.Actor = this;
            vision.Init();
        }

        if (weapons)
        {
            weapons.OnInitialize();
        }

        if (health)
        {
            health.SetMaxHealth(characteristics["max_health"].Value);
            health.onDie += OnDie;
            health.onHealthChangeBuff += OnHealthChangeAddBuff;
        }

        if (equipments)
        {
            equipments.onEquip += OnEquipItem;
            equipments.onDequip += OnDequipItem;
        }

        if (buffs)
        {
            buffs.Init();
            buffs.onBuffAdd += OnBuffAdd;
            buffs.onBuffRemove += OnBuffRemove;
        }
    }

    public void OnDeinitialize()
    {
        if (inventory)
        {
            inventory.onItemUse -= OnItemUse;
            inventory.onItemEquip -= OnItemEquip;
        }

        if (vision)
        {
            vision.Kill();
        }

        if (health)
        {
            health.onDie -= OnDie;
            health.onHealthChangeBuff -= OnHealthChangeAddBuff;
        }

        if (equipments)
        {
            equipments.onEquip -= OnEquipItem;
            equipments.onDequip -= OnDequipItem;
        }

        if (buffs)
        {
            buffs.onBuffAdd -= OnBuffAdd;
            buffs.onBuffRemove -= OnBuffRemove;
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
        rb.velocity = 100 * 2 * Time.fixedDeltaTime * dir;
    }

    public void MoveAgent(Vector2 targetPoint)
    {
        agent.SetDestination(targetPoint);
    }

    public void ChangeIsStopped(bool value)
    {
        if (!agent.enabled) return;

        agent.isStopped = value;
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

    #region Listeners

    private void OnDie()
    {
        gameObject.SetActive(false);
    }

    private void OnEquipItem(ItemEntity entity)
    {
        foreach ((string key, float value) in entity.Info.ChangingCharacteristics)
        {
            if (characteristics.TryGetCharacteristic(key, out CharacteristicEntity characteristic))
            {
                characteristic.Value += value;
            }
        }
    }

    private void OnDequipItem(ItemEntity entity)
    {
        foreach ((string key, float value) in entity.Info.ChangingCharacteristics)
        {
            if (characteristics.TryGetCharacteristic(key, out CharacteristicEntity characteristic))
            {
                characteristic.Value -= value;
            }
        }
    }

    private void OnBuffAdd(BuffEntity buffData)
    {
        foreach ((string key, float value) in buffData.Data.Characteristics)
        {
            if (characteristics.TryGetCharacteristic(key, out CharacteristicEntity characteristic))
            {
                characteristic.Value += value;
            }
        }
    }

    private void OnBuffRemove(BuffEntity buffData)
    {
        foreach ((string key, float value) in buffData.Data.Characteristics)
        {
            if (characteristics.TryGetCharacteristic(key, out CharacteristicEntity characteristic))
            {
                characteristic.Value -= value;
            }
        }
    }

    private void OnHealthChangeAddBuff(BuffData buff)
    {
        if (!buffs) return;

        buffs.AddBuff(buff);
    }

    private void OnItemUse(ItemEntity item)
    {
        ItemInfo info = item.Info; 

        foreach ((string key, float value) in info.ChangingNeeds)
        {
            if (needs.TryGetNeed(key, out NeedEntity need))
            {
                need.Value += value;
            }
        }

        foreach (BuffInfo buffInfo in info.UseBuffsInfos)
        {
            buffs.AddBuff(buffInfo.BuffData);
        }
    }

    private void OnItemEquip(ItemEntity item)
    {
        equipments.TryEquip(item);
    }

    #endregion

    public void TryInteract()
    {
        interactions.TryInteract(this);
    }

    public void Revive()
    {
        health.Revive();
    }
}
