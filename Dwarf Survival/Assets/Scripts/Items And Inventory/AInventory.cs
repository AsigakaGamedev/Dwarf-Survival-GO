using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInventory : MonoBehaviour, IInitListener
{
    [Header("Crafts")]
    [SerializeField] private CraftInfo[] possibleCrafts;

    [Header("Items")]
    [SerializeField] private ItemData[] startItems;

    [Space]
    [ReadOnly, SerializeField] protected List<InventoryCellEntity> cells;

    public List<InventoryCellEntity> Cells { get => cells; }
    public CraftInfo[] PossibleCrafts { get => possibleCrafts; }

    public void OnInitialize()
    {
        cells = new List<InventoryCellEntity>();
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
                targetCell.ItemInCell = new ItemEntity(data);
            }
        }
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

    

    #endregion
}
