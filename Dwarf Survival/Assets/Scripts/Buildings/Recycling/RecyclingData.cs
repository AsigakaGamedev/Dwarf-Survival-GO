using UnityEngine;

[System.Serializable]
public struct RecyclingRecipeData
{
    [SerializeField] private ItemData inputItem;
    [SerializeField] private ItemData outputItem;

    public ItemData InputItem { get => inputItem; }
    public ItemData OutputItem { get => outputItem; }
}
