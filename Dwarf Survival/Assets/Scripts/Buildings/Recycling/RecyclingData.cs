using UnityEngine;

[System.Serializable]
public struct RecyclingRecipeData
{
    [SerializeField] private float recyclingTime;

    [Space]
    [SerializeField] private ItemData[] inputItems;
    [SerializeField] private ItemData[] outputItems;

    public float RecyclingTime { get => recyclingTime; }

    public ItemData[] InputItems { get => inputItems; }
    public ItemData[] OutputItems { get => outputItems; }
}
