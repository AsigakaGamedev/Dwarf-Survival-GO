using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIRecyclingManager : MonoBehaviour
{
    [SerializeField] private UIInventoryCell cellPrefab;
    [SerializeField] private Transform itemDragParent;

    [Space]
    [SerializeField] private Transform inputCellsContent;

    [Space]
    [SerializeField] private Transform outputCellsContent;

    [Space]
    [SerializeField] private Transform fuelCellsContent;

    [Space]
    [ReadOnly, SerializeField] private RecyclingInteract curRecyclingObject;

    private ObjectPoolingManager poolingManager;

    private List<UIInventoryCell> spawnedInputCells;
    private List<UIInventoryCell> spawnedOutputCells;
    private UIInventoryCell spawnedFuelCell;

    [Inject]
    private void Construct(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }

    private void Start()
    {
        spawnedInputCells = new List<UIInventoryCell>();
        spawnedOutputCells = new List<UIInventoryCell>();

        spawnedFuelCell = poolingManager.GetPoolable(cellPrefab);
        spawnedFuelCell.transform.SetParent(fuelCellsContent);
        spawnedFuelCell.transform.localScale = Vector3.one;
        spawnedFuelCell.SetItemParent(itemDragParent);
    }

    public void OpenRecyclingObject(RecyclingInteract recyclingObject)
    {
        ClearCells();

        curRecyclingObject = recyclingObject;

        foreach (InventoryCellEntity inputCell in recyclingObject.InputCells)
        {
            UIInventoryCell newInputCell = poolingManager.GetPoolable(cellPrefab);
            newInputCell.transform.SetParent(inputCellsContent);
            newInputCell.transform.localScale = Vector3.one;
            newInputCell.SetEntity(inputCell);
            newInputCell.SetItemParent(itemDragParent);
            spawnedInputCells.Add(newInputCell);
        }

        foreach (InventoryCellEntity outputCell in recyclingObject.OutputCells)
        {
            UIInventoryCell newOutputCell = poolingManager.GetPoolable(cellPrefab);
            newOutputCell.transform.SetParent(outputCellsContent);
            newOutputCell.transform.localScale = Vector3.one;
            newOutputCell.SetEntity(outputCell);
            newOutputCell.SetItemParent(itemDragParent);
            spawnedOutputCells.Add(newOutputCell);
        }

        spawnedFuelCell.SetEntity(recyclingObject.FuelCell);
    }

    private void ClearCells()
    {
        foreach (UIInventoryCell spawnedInputCell in spawnedInputCells)
        {
            spawnedInputCell.gameObject.SetActive(false);
        }

        spawnedInputCells.Clear();

        foreach (UIInventoryCell spawnedOutputCell in spawnedOutputCells)
        {
            spawnedOutputCell.gameObject.SetActive(false);
        }

        spawnedOutputCells.Clear();
    }
}
