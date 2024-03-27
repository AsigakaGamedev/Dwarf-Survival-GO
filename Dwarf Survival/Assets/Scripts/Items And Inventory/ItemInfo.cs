using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Info")]
public class ItemInfo : ScriptableObject
{
    [Header("Main")]
    [ShowAssetPreview, SerializeField] private Sprite cellIcon;
    [ShowAssetPreview, SerializeField] private Sprite moveIcon;
    [ShowAssetPreview, ShowIf(nameof(hasEquipmentIcon)), SerializeField] private Sprite equipmentIcon;

    [Header("Equipment")]
    [SerializeField] private bool isEquipable;
    [ShowIf(nameof(isEquipable)), SerializeField] private int equipSlotID;
    [ShowIf(nameof(isEquipable)), SerializeField] private SerializedDictionary<string, float> changingCharacteristics;

    [Header("Weapon")]
    [SerializeField] private bool isWeapon;

    [Header("Fuel")]
    [SerializeField] private bool isFuel;
    [ShowIf(nameof(isFuel)), SerializeField] private int fuelCapacity;

    private bool hasEquipmentIcon => isWeapon || isEquipable;

    public Sprite CellIcon { get => cellIcon; }
    public Sprite MoveIcon { get => moveIcon; }
    public Sprite EquipmentIcon { get => equipmentIcon; }

    public bool IsEquipable { get => isEquipable; }
    public int EquipSlotID { get => equipSlotID; }
    public SerializedDictionary<string, float> ChangingCharacteristics { get => changingCharacteristics; }

    public bool IsWeapon { get => isWeapon; }

    public bool IsFuel { get => isFuel; }
    public int FuelCapacity { get => fuelCapacity; }
}
