using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Zenject;

public enum BuildingType { Other, Craft, Defence }

public class BuildingsManager : MonoBehaviour
{
    [SerializeField] private BuildingObject[] allBuildingsPrefabs;

    [Space]
    [ReadOnly, SerializeField] private BuildingObject selectedBuilding;

    private ObjectPoolingManager poolingManager;
    private PlayerManager playerManager;
    private AInventory playerInventory;
    private Camera mainCamera;

    public BuildingObject[] AllBuildingsPrefabs { get => allBuildingsPrefabs; }

    [Inject]
    private void Construct(PlayerManager playerManager, ObjectPoolingManager poolingManager)
    {
        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;

        this.poolingManager = poolingManager;
    }

    private void OnDestroy()
    {
        if (playerManager)
        {
            playerManager.onPlayerSpawn -= OnPlayerSpawn;
        }
    }

    private void OnPlayerSpawn(PlayerActorController playerActor)
    {
        playerInventory = playerActor.Actor.Inventory;
    }

    private void Start()
    {
        mainCamera = Camera.main;
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
