using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInventory : MonoBehaviour, IInitListener
{
    [SerializeField] private ItemData[] startItems;

    [Space]
    [ReadOnly, SerializeField] protected List<InventoryCellEntity> cells;

    public List<InventoryCellEntity> Cells { get => cells; }

    public void OnInitialize()
    {
        cells = new List<InventoryCellEntity>();
    }

    public void AddStartItems()
    {
        foreach (ItemData startItem in startItems)
        {
            AddItem(startItem);
        }
    }

    public void SetCellsCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            InventoryCellEntity newCell = new InventoryCellEntity();
            cells.Add(newCell);
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
}
