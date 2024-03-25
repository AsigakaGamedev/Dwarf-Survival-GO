using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public enum InventoryCellType { All, OnlyFuel }

[System.Serializable]
public class InventoryCellEntity 
{
    [SerializeField] private ItemEntity itemInCell;

    private InventoryCellType cellType;

    public InventoryCellEntity()
    {
        cellType = InventoryCellType.All;
    }

    public InventoryCellEntity(InventoryCellType cellType)
    {
        this.cellType = cellType;
    }

    public ItemEntity ItemInCell { get => itemInCell; }

    public InventoryCellType CellType { get => cellType; }

    public bool TryAddItem(ItemEntity newItem)
    {
        if (!CanAddItem(newItem)) return false;

        if (itemInCell != null)
        {
            itemInCell.onAmountChange -= OnItemAmountChange;
        }

        itemInCell = newItem;

        if (itemInCell != null) itemInCell.onAmountChange += OnItemAmountChange;

        return true;
    }

    public bool CanAddItem(ItemEntity newItem)
    {
        if (cellType == InventoryCellType.OnlyFuel &&
                newItem != null && newItem.Info != null &&
                !newItem.Info.IsFuel) return false;

        return true;
    }

    private void OnItemAmountChange(int amount)
    {
        if (amount <= 0)
        {
            TryAddItem(null);
        }
    }
}
