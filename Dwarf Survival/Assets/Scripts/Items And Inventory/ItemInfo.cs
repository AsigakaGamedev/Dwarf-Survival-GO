using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Info")]
public class ItemInfo : ScriptableObject
{
    [ShowAssetPreview, SerializeField] private Sprite cellIcon;
    [ShowAssetPreview, SerializeField] private Sprite moveIcon;
    [ShowAssetPreview, ShowIf(nameof(isEquipable)), SerializeField] private Sprite equipmentIcon;

    [Space]
    [SerializeField] private bool isEquipable;
    [ShowIf(nameof(isEquipable)), SerializeField] private int equipSlotID;

    public Sprite CellIcon { get => cellIcon; }
    public Sprite MoveIcon { get => moveIcon; }
    public Sprite EquipmentIcon { get => equipmentIcon; }

    public bool IsEquipable { get => isEquipable; }
    public int EquipSlotID { get => equipSlotID; }
}
