using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsController : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] slots;

    public bool TryEquip(ItemEntity item)
    {
        if (!item.Info.IsEquipable) return false;

        EquipmentSlot slot = GetSlot(item.Info.EquipSlotID);
        slot.Equip(item);
        return true;
    }

    public bool TryEquip(ItemEntity item, int slotID)
    {
        if (!item.Info.IsEquipable && item.Info.EquipSlotID != slotID) return false;

        EquipmentSlot slot = GetSlot(slotID);
        slot.Equip(item);
        return true;
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
