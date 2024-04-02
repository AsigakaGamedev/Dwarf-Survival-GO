using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Info")]
public class ItemInfo : ScriptableObject
{
    [Header("Main")]
    [SerializeField] private string nameKey; 
    [SerializeField] private string descKey; 

    [Space]
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

    [Header("Use")]
    [SerializeField] private bool isUsable;
    [SerializeField] private BuffData useBuffsDatas;
    [SerializeField] private BuffInfo useBuffsInfos;

    private bool hasEquipmentIcon => isWeapon || isEquipable;

    public string NameKey { get => nameKey; }
    public string DescKey { get => descKey; }

    public Sprite CellIcon { get => cellIcon; }
    public Sprite MoveIcon { get => moveIcon; }
    public Sprite EquipmentIcon { get => equipmentIcon; }

    public bool IsEquipable { get => isEquipable; }
    public int EquipSlotID { get => equipSlotID; }
    public SerializedDictionary<string, float> ChangingCharacteristics { get => changingCharacteristics; }

    public bool IsWeapon { get => isWeapon; }

    public bool IsFuel { get => isFuel; }
    public int FuelCapacity { get => fuelCapacity; }

    public bool IsUsable { get => isUsable; }
    public BuffData UseBuffsDatas { get => useBuffsDatas; }
    public BuffInfo UseBuffsInfos { get => useBuffsInfos; }
}
