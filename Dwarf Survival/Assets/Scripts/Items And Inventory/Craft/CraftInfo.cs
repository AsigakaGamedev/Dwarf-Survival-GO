using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CraftType { Other, Resources, Weapons, Foods }

[CreateAssetMenu(menuName ="Items/Craft")]
public class CraftInfo : ScriptableObject
{
    [ShowAssetPreview, SerializeField] private Sprite recipeIcon;

    [Space]
    [SerializeField] private CraftType type;

    [Space]
    [SerializeField] private ItemData[] neededItems;
    [SerializeField] private ItemData[] resultItems;

    public Sprite RecipeIcon { get => recipeIcon; }

    public CraftType Type { get => type; }

    public ItemData[] NeededItems { get => neededItems; }
    public ItemData[] ResultItems { get => resultItems; }
}
