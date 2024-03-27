using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIRecyclingManager : MonoBehaviour
{
    [SerializeField] private UIInventoryCell cellPrefab;
    [SerializeField] private Transform itemDragParent;
    [SerializeField] private Slider recyclingProgressSlider;

    [Space]
    [SerializeField] private Transform inputCellsContent;
    [SerializeField] private Transform outputCellsContent;
    [SerializeField] private Transform fuelCellsContent;

    [Space]
    [ReadOnly, SerializeField] private RecyclingInteract curRecyclingObject;

    private ObjectPoolingManager poolingManager;

    private UIInventoryCell spawnedInputCells;
    private UIInventoryCell spawnedOutputCells;
    private UIInventoryCell spawnedFuelCell;

    [Inject]
    private void Construct(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }

    private void Awake()
    {
        recyclingProgressSlider.value = 0;
        recyclingProgressSlider.minValue = 0;
        recyclingProgressSlider.maxValue = 100;

        spawnedFuelCell = poolingManager.GetPoolable(cellPrefab);
        spawnedFuelCell.transform.SetParent(fuelCellsContent);
        spawnedFuelCell.transform.localScale = Vector3.one;
        spawnedFuelCell.SetItemParent(itemDragParent);

        spawnedInputCells = poolingManager.GetPoolable(cellPrefab);
        spawnedInputCells.transform.SetParent(inputCellsContent);
        spawnedInputCells.transform.localScale = Vector3.one;
        spawnedInputCells.SetItemParent(itemDragParent);

        spawnedOutputCells = poolingManager.GetPoolable(cellPrefab);
        spawnedOutputCells.transform.SetParent(outputCellsContent);
        spawnedOutputCells.transform.localScale = Vector3.one;
        spawnedOutputCells.SetItemParent(itemDragParent);
    }

    private void OnRecyclingProgressChange(float progress)
    {
        recyclingProgressSlider.value = progress;   
    }

    public void OpenRecyclingObject(RecyclingInteract recyclingObject)
    {
        if (curRecyclingObject)
        {
            curRecyclingObject.onRecyclingProgressChange -= OnRecyclingProgressChange;
        }

        curRecyclingObject = recyclingObject;
        curRecyclingObject.onRecyclingProgressChange += OnRecyclingProgressChange;
        recyclingProgressSlider.value = curRecyclingObject.RecyclingProgress;

        spawnedFuelCell.SetEntity(recyclingObject.FuelCell);
        spawnedInputCells.SetEntity(recyclingObject.InputCell);
        spawnedOutputCells.SetEntity(recyclingObject.OutputCell);
    }
}
