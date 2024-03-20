using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemData 
{
    [SerializeField] private ItemInfo info;
    [SerializeField] private Vector2Int amountRange;

    public ItemInfo Info { get => info; }
    public int RandomAmount { get => Random.Range(amountRange.x, amountRange.y); }
}
