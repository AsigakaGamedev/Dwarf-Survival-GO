using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIBuildingsPanel : MonoBehaviour
{
    [SerializeField] private UIBuildingsType[] typesBtns;

    [Space]
    [SerializeField] private UIBuildingItem itemPrefab;
    [SerializeField] private Transform itemsContent;

    [Space]
    [SerializeField] private GameObject buildingsPanel;

    [Header("Selected Building")]
    [SerializeField] private GameObject selectedBuilding;
    [SerializeField] private TextMeshProUGUI selectedBuildingName;
    [SerializeField] private TextMeshProUGUI selectedBuildingDescription;

    [Space]
    [SerializeField] private UICraftNeededItem neededItemPrefab;
    [SerializeField] private Transform neededItemsContent;

    private BuildingsManager buildingsManager;
    private ObjectPoolingManager poolingManager;
    private PlayerManager playerManager;
    private AInventory playerInventory;

    private List<UIBuildingItem> spawnedListItems;
    private List<UICraftNeededItem> spawnedNeededItems;

    private EventSystem eventSystem;

    [Inject]
    public void Construct(BuildingsManager buildingsManager, PlayerManager playerManager)
    {
        this.buildingsManager = buildingsManager;

        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;
    }

    private void OnEnable()
    {
        buildingsPanel.SetActive(false);
        selectedBuilding.SetActive(false);
    }

    private void Start()
    {
        eventSystem = EventSystem.current;

        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();

        spawnedListItems = new List<UIBuildingItem>();
        spawnedNeededItems = new List<UICraftNeededItem>();

        foreach (var type in typesBtns)
        {
            type.onTypeChange += OnTypeChange;
        }

        OnTypeChange(BuildingType.Other);
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

    private void OnTypeChange(BuildingType type)
    {
        foreach (UIBuildingItem spawnedItem in spawnedListItems)
        {
            spawnedItem.onBeginDrag -= OnBeginDrag;
            spawnedItem.onDrag -= OnDrag;
            spawnedItem.onEndDrag -= OnEndDrag;
            spawnedItem.onClick -= OnClick;
            spawnedItem.gameObject.SetActive(false);
        }

        spawnedListItems.Clear();

        foreach (BuildingObject prefab in buildingsManager.AllBuildingsPrefabs)
        {
            if (prefab.Type == type)
            {
                UIBuildingItem newListItem = poolingManager.GetPoolable(itemPrefab);
                newListItem.transform.SetParent(itemsContent);
                newListItem.transform.localScale = Vector3.one;
                newListItem.SetPrefab(prefab);

                newListItem.onBeginDrag += OnBeginDrag;
                newListItem.onDrag += OnDrag;
                newListItem.onEndDrag += OnEndDrag;
                newListItem.onClick += OnClick;

                spawnedListItems.Add(newListItem);
            }
        }

        buildingsPanel.SetActive(true);
        selectedBuilding.SetActive(false);
    }

    private void OnBeginDrag(UIBuildingItem buildingItem)
    {
        buildingItem.IconImg.transform.SetParent(transform);
        buildingItem.IconImg.raycastTarget = false;
        buildingItem.SetIconAlpha(0.75f);

        buildingsManager.SelectBuildingPrefab(buildingItem.LinkedPrefab);
    }

    private void OnDrag(UIBuildingItem buildingItem)
    {
        buildingItem.IconImg.transform.position = Input.mousePosition;

        buildingsManager.MoveSelectedBuilding(Input.mousePosition);

        buildingItem.IconImg.gameObject.SetActive(eventSystem.IsPointerOverGameObject());
    }

    private void OnEndDrag(UIBuildingItem buildingItem)
    {
        buildingItem.IconImg.gameObject.SetActive(true);
        buildingItem.IconImg.transform.SetParent(buildingItem.transform);
        buildingItem.IconImg.raycastTarget = true;
        buildingItem.SetIconAlpha(1);

        RectTransform rect = buildingItem.IconImg.transform as RectTransform;
        rect.anchoredPosition = Vector3.zero;

        if (eventSystem.IsPointerOverGameObject())
        {
            buildingsManager.DeselectSelectedBuilding();
        }
        else
        {
            buildingsManager.ConstructSelectedPrefab();
        }
    }

    private void OnClick(UIBuildingItem buildingItem)
    {
        selectedBuilding.SetActive(true);

        BuildingObject prefab = buildingItem.LinkedPrefab;
        selectedBuildingName.text = prefab.BuildingName;
        selectedBuildingDescription.text = prefab.BuildingDesc;

        foreach (UICraftNeededItem spawnedItem in spawnedNeededItems)
        {
            spawnedItem.gameObject.SetActive(false);
        }

        spawnedNeededItems.Clear();

        foreach (ItemData neededItem in prefab.NeededItems)
        {
            UICraftNeededItem newUIItem = poolingManager.GetPoolable(neededItemPrefab);
            newUIItem.transform.SetParent(neededItemsContent);
            newUIItem.transform.localScale = Vector3.one;

            playerInventory.HasItem(neededItem, out int curAmount);

            newUIItem.SetData(neededItem.Info.CellIcon, curAmount, neededItem.RandomAmount); 
            spawnedNeededItems.Add(newUIItem);
        }
    }
}
