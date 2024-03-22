using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryCellEntity 
{
    [SerializeField] private ItemEntity itemInCell;

    public ItemEntity ItemInCell { get => itemInCell; 
        set
        {
            if (itemInCell != null)
            {
                itemInCell.onAmountChange -= OnItemAmountChange;
            }

            itemInCell = value;

            if (itemInCell != null)
            {
                itemInCell.onAmountChange += OnItemAmountChange;
            }
        }
    }

    private void OnItemAmountChange(int amount)
    {
        if (amount <= 0)
        {
            ItemInCell = null;
        }
    }
}
