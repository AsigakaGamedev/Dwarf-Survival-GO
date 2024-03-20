using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBuildingsPanel : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private UIBuildingsType[] typesBtns;

    [Space]
    [SerializeField] private UIBuildingItem itemPrefab;
    [SerializeField] private Transform itemsContent;

    [Space]
    [SerializeField] private GameObject buildingsPanel;

    private BuildingsManager buildingsManager;
    private ObjectPoolingManager poolingManager;

    private List<UIBuildingItem> spawnedListItems;

    private EventSystem eventSystem;

    private void OnEnable()
    {
        buildingsPanel.SetActive(false);
    }

    public void OnInitialize()
    {
        eventSystem = EventSystem.current;

        buildingsManager = BuildingsManager.Instance;
        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();

        spawnedListItems = new List<UIBuildingItem>();

        foreach (var type in typesBtns)
        {
            type.onTypeChange += OnTypeChange;
        }

        OnTypeChange(BuildingType.Other);

        print("UI Buildings Panel инициализирован");
    }

    public void OnDeinitialize()
    {
        foreach (var type in typesBtns)
        {
            type.onTypeChange -= OnTypeChange;
        }
    }

    private void OnTypeChange(BuildingType type)
    {
        foreach (UIBuildingItem spawnedItem in spawnedListItems)
        {
            spawnedItem.onBeginDrag -= OnBeginDrag;
            spawnedItem.onDrag -= OnDrag;
            spawnedItem.onEndDrag -= OnEndDrag;
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

                spawnedListItems.Add(newListItem);
            }
        }

        buildingsPanel.SetActive(true);
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
}
