using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<ItemInfo, WeaponModel> allWeapons;

    [Space]
    [ReadOnly, SerializeField] private ItemEntity equipedItem;
    [ReadOnly, SerializeField] private WeaponModel equipedWeapon;

    public bool TryEquip(ItemEntity item)
    {
        equipedItem = item;

        if (!equipedItem.Info.IsWeapon) return false;

        if (equipedWeapon) equipedWeapon.OnDequip();

        equipedWeapon = allWeapons[equipedItem.Info];
        equipedWeapon.OnEquip();
        return true;
    }
}
