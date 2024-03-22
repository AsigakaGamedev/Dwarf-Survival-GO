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
    [SerializeField] private string recipeName;
    [SerializeField] private string recipeDesc;

    [Space]
    [SerializeField] private CraftType type;

    [Space]
    [SerializeField] private ItemData[] neededItems;
    [SerializeField] private ItemData[] resultItems;

    public Sprite RecipeIcon { get => recipeIcon; }

    public string RecipeName { get => recipeName; }
    public string RecipeDesc { get => recipeDesc; }

    public CraftType Type { get => type; }

    public ItemData[] NeededItems { get => neededItems; }
    public ItemData[] ResultItems { get => resultItems; }
}
