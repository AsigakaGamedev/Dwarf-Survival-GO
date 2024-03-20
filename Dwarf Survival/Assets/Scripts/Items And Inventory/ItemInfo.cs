using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Info")]
public class ItemInfo : ScriptableObject
{
    [ShowAssetPreview, SerializeField] private Sprite cellIcon;
    [ShowAssetPreview, SerializeField] private Sprite moveIcon;

    public Sprite CellIcon { get => cellIcon; }
    public Sprite MoveIcon { get => moveIcon; }
}
