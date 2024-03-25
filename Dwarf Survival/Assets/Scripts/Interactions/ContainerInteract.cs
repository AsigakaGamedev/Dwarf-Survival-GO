using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(InventoryController))]
public class ContainerInteract : AInteractionComponent
{
    [SerializeField] private int cellsCount;

    [Space]
    [SerializeField] private InventoryController containerInventory;

    private UIManager uiManager;
    private UIInventoriesManager uiInventoriesManager;

    private void OnValidate()
    {
        if (!containerInventory) containerInventory = GetComponent<InventoryController>();
    }

    [Inject]
    private void Construct(UIManager uiManager, UIInventoriesManager uiInventoriesManager)
    {
        this.uiManager = uiManager;
        this.uiInventoriesManager = uiInventoriesManager;
    }

    private void Start()
    {
        containerInventory.Init();
        containerInventory.SetCellsCount(cellsCount);
        containerInventory.AddStartItems();
    }

    public override void OnInteract(Actor actor)
    {
        uiManager.ChangeScreen("container");
        uiInventoriesManager.GetPanel("container").ShowInventory(containerInventory.Cells);
    }
}
