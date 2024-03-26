using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RecyclingInteract : AInteractionComponent
{
    [SerializeField] private float recyclingModifier = 1;
    [SerializeField] private int inputCellsCount = 1;
    [SerializeField] private int outputCellsCount = 1;

    [Space]
    [SerializeField] private RecyclingRecipeData[] recipies;

    [Space]
    [ReadOnly, SerializeField] private InventoryCellEntity[] inputCells;
    [ReadOnly, SerializeField] private InventoryCellEntity[] outputCells;
    [ReadOnly, SerializeField] private InventoryCellEntity fuelCell;

    private UIRecyclingManager uiRecyclingManager;
    private UIManager uiManager;

    public InventoryCellEntity[] InputCells { get => inputCells; }
    public InventoryCellEntity[] OutputCells { get => outputCells; }
    public InventoryCellEntity FuelCell { get => fuelCell; }

    [Inject]
    private void Construct(UIRecyclingManager uiRecyclingManager, UIManager uiManager)
    {
        this.uiRecyclingManager = uiRecyclingManager;
        this.uiManager = uiManager;
    }

    private void Start()
    {
        inputCells = new InventoryCellEntity[inputCellsCount];
        outputCells = new InventoryCellEntity[outputCellsCount];
        fuelCell = new InventoryCellEntity(InventoryCellType.OnlyFuel);

        for (int i = 0; i < inputCellsCount; i++)
        {
            inputCells[i] = new InventoryCellEntity();
        }

        for (int i = 0; i < outputCellsCount; i++)
        {
            outputCells[i] = new InventoryCellEntity();
        }
    }

    private void OnDestroy()
    {
        
    }

    public override void OnInteract(Actor actor)
    {
        uiRecyclingManager.OpenRecyclingObject(this);
        uiManager.ChangeScreen("recycling");
    }
}
