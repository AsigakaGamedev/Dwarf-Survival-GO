using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsController : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] slots;

    public Action<ItemEntity> onEquip;
    public Action<ItemEntity> onDequip;

    public bool TryEquip(ItemEntity item)
    {
        if (item == null || item.Info == null) return false;

        return TryEquip(item, item.Info.EquipSlotID);
    }

    public bool TryEquip(ItemEntity item, int slotID)
    {
        if (!item.Info.IsEquipable && item.Info.EquipSlotID != slotID) return false;

        EquipmentSlot slot = GetSlot(slotID);

        if (slot.EquipedEntity != null)
        {
            onDequip?.Invoke(slot.EquipedEntity);
            slot.DequipCurrent();
        }

        slot.Equip(item);
        onEquip?.Invoke(item);
        return true;
    }

    public bool TryDequip(int slotID)
    {
        EquipmentSlot slot = GetSlot(slotID);

        if (slot.EquipedEntity != null)
        {
            onDequip?.Invoke(slot.EquipedEntity);
            slot.DequipCurrent();
            return true; 
        }

        return false;
    }

    public EquipmentSlot GetSlot(int id)
    {
        foreach (var slot in slots)
        {
            if (slot.Id == id) return slot;
        }

        return null;
    }
}
