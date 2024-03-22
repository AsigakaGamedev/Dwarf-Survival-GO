using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEntity 
{
    [SerializeField] private ItemInfo info;
    [SerializeField] private int amount;

    public Action<int> onAmountChange;

    public ItemEntity(ItemData data)
    {
        this.info = data.Info;
        this.amount = data.RandomAmount;
    }

    public ItemEntity(ItemInfo info, int amount)
    {
        this.info = info;
        this.amount = amount;
    }

    public ItemInfo Info { get => info; set => info = value; }
    public int Amount { get => amount;
        set
        {
            amount = value;
            onAmountChange?.Invoke(amount);
        }
    }
}
