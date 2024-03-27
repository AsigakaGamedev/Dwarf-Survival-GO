using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RecyclingInteract : AInteractionComponent
{
    [SerializeField] private float recyclingModifier = 1;

    [Space]
    [SerializeField] private RecyclingRecipeData[] recipies;

    [Space]
    [ReadOnly, SerializeField] private InventoryCellEntity inputCell;
    [ReadOnly, SerializeField] private InventoryCellEntity outputCell;
    [ReadOnly, SerializeField] private InventoryCellEntity fuelCell;

    [Space]
    [ReadOnly, SerializeField] private float recyclingProgress;
    [ReadOnly, SerializeField] private int curRecipeIndex;

    private UIRecyclingManager uiRecyclingManager;
    private UIManager uiManager;

    public Action<float> onRecyclingProgressChange;

    public InventoryCellEntity InputCell { get => inputCell; }
    public InventoryCellEntity OutputCell { get => outputCell; }
    public InventoryCellEntity FuelCell { get => fuelCell; }
    public float RecyclingProgress { get => recyclingProgress;
        private set
        {
            recyclingProgress = value;
            onRecyclingProgressChange?.Invoke(recyclingProgress);
        }
    }

    [Inject]
    private void Construct(UIRecyclingManager uiRecyclingManager, UIManager uiManager)
    {
        this.uiRecyclingManager = uiRecyclingManager;
        this.uiManager = uiManager;
    }

    private void Start()
    {
        curRecipeIndex = -1;

        inputCell = new InventoryCellEntity();
        outputCell = new InventoryCellEntity();
        fuelCell = new InventoryCellEntity(InventoryCellType.OnlyFuel);

        inputCell.onItemChange += OnInputItemChange;
    }

    private void Update()
    {
        if (curRecipeIndex == -1) return;

        RecyclingProgress += Time.deltaTime * recyclingModifier;

        if (RecyclingProgress >= 100)
        {
            outputCell.TryAddItem(new ItemEntity(recipies[curRecipeIndex].OutputItem.Info,
                                                 recipies[curRecipeIndex].OutputItem.RandomAmount));
            inputCell.ItemInCell.Amount--;
            
            RecyclingProgress = 0;
            curRecipeIndex = -1;
            fuelCell.ItemInCell.FuelCapacity--;

            TryRecyclingInputItem();
        }
    }

    private void OnDestroy()
    {
        inputCell.onItemChange -= OnInputItemChange;
    }

    public override void OnInteract(Actor actor)
    {
        uiManager.ChangeScreen("recycling");
        uiRecyclingManager.OpenRecyclingObject(this);
    }

    private void TryRecyclingInputItem()
    {
        OnInputItemChange(inputCell.ItemInCell);
    }

    private void OnInputItemChange(ItemEntity item)
    {
        if (item == null || 
            fuelCell.ItemInCell == null)
        {
            curRecipeIndex = -1;
            RecyclingProgress = 0;
            return;
        }

        if (TryGetRecipe(item.Info, out curRecipeIndex))
        {
            RecyclingProgress = 0;
        }
    }

    private bool TryGetRecipe(ItemInfo inputInfo, out int recyclingRecipeIndex)
    {
        for (int i = 0; i < recipies.Length; i++)
        {
            if (recipies[i].InputItem.Info == inputInfo)
            {
                recyclingRecipeIndex = i;
                return true;
            }
        }

        recyclingRecipeIndex = -1;
        return false;
    }
}
