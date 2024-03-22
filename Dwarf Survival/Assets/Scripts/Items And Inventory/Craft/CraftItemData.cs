using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CraftItemData 
{
    [SerializeField] private ItemInfo info;
    [SerializeField] private int amount;

    public ItemInfo Info { get => info; }
    public int Amount { get => amount; }
}
