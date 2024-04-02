using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("Crafts")]
    [SerializeField] private CraftInfo[] possibleCrafts;

    [Header("Items")]
    [SerializeField] private ItemData[] startItems;

    protected List<InventoryCellEntity> cells;

    public Action<ItemEntity> onItemUse;
    public Action<ItemEntity> onItemEquip;

    public List<InventoryCellEntity> Cells { get => cells; }
    public CraftInfo[] PossibleCrafts { get => possibleCrafts; }

    public void Init()
    {
        cells = new List<InventoryCellEntity>();
    }

    private void OnDestroy()
    {
        foreach (InventoryCellEntity cell in cells)
        {
            cell.onItemUse -= OnItemUse;
            cell.onItemEquip -= OnItemEquip;
            cell.onItemDrop -= OnItemDrop;
        }
    }

    #region Items

    public void AddStartItems()
    {
        foreach (ItemData startItem in startItems)
        {
            AddItem(startItem);
        }
    }

    public void AddItem(ItemData data)
    {
        InventoryCellEntity targetCell = GetCell(data.Info);

        if (targetCell != null)
        {
            targetCell.ItemInCell.Amount += data.RandomAmount;
        }
        else
        {
            targetCell = GetFreeCell();

            if (targetCell != null)
            {
                targetCell.TryAddItem(new ItemEntity(data));
            }
        }
    }

    public void RemoveItem(ItemData itemData)
    {
        InventoryCellEntity cell = GetCell(itemData.Info);

        cell.ItemInCell.Amount -= itemData.RandomAmount;
    }

    public bool HasItem(ItemData searchItem, out int amount)
    {
        InventoryCellEntity cell = GetCell(searchItem.Info);
        amount = 0;

        if (cell == null) return false;

        amount = cell.ItemInCell.Amount;
        if (cell.ItemInCell.Amount < searchItem.RandomAmount) return false;

        return true;
    }

    #endregion

    #region Cells

    public void SetCellsCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            InventoryCellEntity newCell = new InventoryCellEntity();
            cells.Add(newCell);

            newCell.onItemUse += OnItemUse;
            newCell.onItemEquip += OnItemEquip;
            newCell.onItemDrop += OnItemDrop;
        }
    }

    public InventoryCellEntity GetFreeCell()
    {
        foreach (InventoryCellEntity cell in cells)
        {
            if (cell.ItemInCell == null || cell.ItemInCell.Info == null) return cell;
        }

        return null;
    }

    public InventoryCellEntity GetCell(ItemInfo info)
    {
        foreach (InventoryCellEntity cell in cells)
        {
            if (cell.ItemInCell != null && cell.ItemInCell.Info != null && cell.ItemInCell.Info == info) return cell;
        }

        return null;
    }

    #endregion

    #region Craft

    public bool TryCraft(CraftInfo recipe)
    {
        if (CanCraft(recipe))
        {
            foreach (ItemData neededItem in recipe.NeededItems)
            {
                RemoveItem(neededItem);
            }

            foreach (ItemData resultItem in recipe.ResultItems)
            {
                AddItem(resultItem);
            }

            return true;
        }

        return false;
    }

    public bool CanCraft(CraftInfo recipe)
    {
        foreach (ItemData neededItem in recipe.NeededItems)
        {
            if (!HasItem(neededItem, out int amount)) return false;
        }

        return true;
    }

    #endregion

    #region Listeners

    private void OnItemUse(ItemEntity item)
    {
        onItemUse?.Invoke(item);
    }

    private void OnItemEquip(ItemEntity item)
    {
        onItemEquip?.Invoke(item);
    }

    private void OnItemDrop(ItemEntity item)
    {
        print("Выкинул");
    }

    #endregion
}
