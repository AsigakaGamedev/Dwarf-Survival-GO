using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIInventoryPanel : MonoBehaviour
{
    [SerializeField] private UIInventoryCell cellPrefab;
    [SerializeField] private Transform cellsContent;
    [SerializeField] private Transform itemDragParent;

    private ObjectPoolingManager poolingManager;
    private List<UIInventoryCell> spawnedCells;

    [Inject]
    public void Construct(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }

    public void Init()
    {
        spawnedCells = new List<UIInventoryCell>();
    }

    public void ShowInventory(List<InventoryCellEntity> cells)
    {
        foreach (UIInventoryCell spawned in spawnedCells)
        {
            spawned.gameObject.SetActive(false);
        }

        spawnedCells.Clear();

        foreach (InventoryCellEntity cellEntity in cells)
        {
            UIInventoryCell newCell = poolingManager.GetPoolable(cellPrefab);
            newCell.transform.SetParent(cellsContent);
            newCell.transform.localScale = Vector3.one;
            newCell.SetEntity(cellEntity);
            newCell.SetItemParent(itemDragParent);
            spawnedCells.Add(newCell);
        }
    }
}
