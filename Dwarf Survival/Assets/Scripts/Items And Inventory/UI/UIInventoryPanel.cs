using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour, IInitListener
{
    [SerializeField] private UIInventoryCell cellPrefab;
    [SerializeField] private Transform cellsContent;
    [SerializeField] private Transform itemDragParent;

    private ObjectPoolingManager poolingManager;
    private List<UIInventoryCell> spawnedCells;

    public void OnInitialize()
    {
        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
        spawnedCells = new List<UIInventoryCell>();
    }

    public void ShowInventory(AInventory inventory)
    {
        foreach (UIInventoryCell spawned in spawnedCells)
        {
            spawned.gameObject.SetActive(false);
        }

        spawnedCells.Clear();

        foreach (InventoryCellEntity cellEntity in inventory.Cells)
        {
            UIInventoryCell newCell = poolingManager.GetPoolable(cellPrefab);
            newCell.transform.SetParent(cellsContent);
            newCell.transform.localScale = Vector3.one;
            newCell.SetEntity(cellEntity);
            newCell.SetItemParen(itemDragParent);
            spawnedCells.Add(newCell);
        }
    }
}
