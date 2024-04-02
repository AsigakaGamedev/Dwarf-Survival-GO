using System;
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

    public Action<ItemEntity> onItemChange;
    public Action<ItemEntity> onItemUse;
    public Action<ItemEntity> onItemDrop;
    public Action<ItemEntity> onItemEquip;
    public Action<int> onItemAmountChange;

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
            if (newItem != null && itemInCell.Info == newItem.Info)
            {
                itemInCell.Amount += newItem.Amount;
                return true;
            }

            itemInCell.onAmountChange -= OnItemAmountChange;
            itemInCell.onUse -= OnItemUse;
            itemInCell.onEquip -= OnItemEquip;
            itemInCell.onDrop -= OnItemDrop;
        }

        itemInCell = newItem;
        onItemChange?.Invoke(itemInCell);

        if (itemInCell != null)
        {
            itemInCell.onAmountChange += OnItemAmountChange;
            itemInCell.onUse += OnItemUse;
            itemInCell.onEquip += OnItemEquip;
            itemInCell.onDrop += OnItemDrop;
        }

        return true;
    }

    public bool CanAddItem(ItemEntity newItem)
    {
        if (cellType == InventoryCellType.OnlyFuel &&
                newItem != null && newItem.Info != null &&
                !newItem.Info.IsFuel) return false;

        return true;
    }

    #region Listeners

    private void OnItemAmountChange(int amount)
    {
        if (amount <= 0)
            TryAddItem(null);
        else
            onItemAmountChange?.Invoke(amount);
    }

    private void OnItemUse(ItemEntity item)
    {
        onItemUse?.Invoke(item);
    }

    private void OnItemDrop(ItemEntity item)
    {
        onItemDrop?.Invoke(item);
    }

    private void OnItemEquip(ItemEntity item)
    {
        onItemEquip?.Invoke(item);
    }

    #endregion
}
