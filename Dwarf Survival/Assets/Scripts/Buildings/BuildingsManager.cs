using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType { Other, Craft, Defence }

public class BuildingsManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private BuildingObject[] allBuildingsPrefabs;

    [Space]
    [ReadOnly, SerializeField] private BuildingObject selectedBuilding;

    private ObjectPoolingManager poolingManager;
    private AInventory playerInventory;
    private Camera mainCamera;

    public static BuildingsManager Instance;

    public BuildingObject[] AllBuildingsPrefabs { get => allBuildingsPrefabs; }

    public void OnInitialize()
    {
        Instance = this;

        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();
        playerInventory = PlayerManager.Instance.PlayerInstance.Actor.Inventory;
        mainCamera = Camera.main;

        print("Buildings Manager инициализирован");
    }

    public void OnDeinitialize()
    {
        Instance = null;
    }

    public void SelectBuildingPrefab(BuildingObject prefab)
    {
        DeselectSelectedBuilding();

        selectedBuilding = poolingManager.GetPoolable(prefab);
        selectedBuilding.SetState(BuildingState.Planning);
    }

    public void DeselectSelectedBuilding()
    {
        if (!selectedBuilding) return;

        selectedBuilding.gameObject.SetActive(false);
        selectedBuilding = null;
    }

    public void MoveSelectedBuilding(Vector3 mousePos)
    {
        if (!selectedBuilding) return;

        Vector3 movePos = mainCamera.ScreenToWorldPoint(mousePos);
        movePos.z = 0;
        selectedBuilding.transform.position = movePos;
    }

    public void ConstructSelectedPrefab()
    {
        if (!selectedBuilding) return;

        if (CanCraftSelected())
        {
            selectedBuilding.SetState(BuildingState.Deactivated);
            selectedBuilding = null;
        }
        else
        {
            selectedBuilding.gameObject.SetActive(false);
            selectedBuilding = null;
        }
    }

    private bool CanCraftSelected()
    {
        if (!selectedBuilding) return false;

        foreach (ItemData neededItem in selectedBuilding.NeededItems)
        {
            if (!playerInventory.HasItem(neededItem, out int amount)) return false;
        }

        return true;
    }
}
