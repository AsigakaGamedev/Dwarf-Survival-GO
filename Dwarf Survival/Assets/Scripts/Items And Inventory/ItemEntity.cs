using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemEntity 
{
    [SerializeField] private ItemInfo info;
    [SerializeField] private int amount;

    private int fuelCapacity;

    public Action<int> onAmountChange;

    public ItemEntity(ItemData data)
    {
        info = data.Info;
        amount = data.RandomAmount;

        if (info.IsFuel)
            fuelCapacity = info.FuelCapacity;
        else
            fuelCapacity = -1;
    }

    public ItemEntity(ItemInfo info, int amount)
    {
        this.info = info;
        this.amount = amount;

        if (info.IsFuel)
            fuelCapacity = info.FuelCapacity;
        else
            fuelCapacity = -1;
    }

    public ItemInfo Info { get => info; set => info = value; }
    public int Amount { get => amount;
        set
        {
            amount = value;
            onAmountChange?.Invoke(amount);
        }
    }

    public int FuelCapacity { get => fuelCapacity;
        set
        {
            fuelCapacity = value;

            if (fuelCapacity <= 0)
            {
                Amount--;
            }
        }
    }
}
