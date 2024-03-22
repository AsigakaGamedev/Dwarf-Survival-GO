using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerCraftsManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private UICraftType[] types;

    [Header("Recipies")]
    [SerializeField] private UICraftRecipe recipePrefab;
    [SerializeField] private Transform recipiesContent;

    [Header("Selected Craft")]
    [SerializeField] private GameObject selectedCraftPanel;
    [SerializeField] private TextMeshProUGUI selectedCraftName;
    [SerializeField] private TextMeshProUGUI selectedCraftDesc;

    [Space]
    [SerializeField] private Button craftSelectedBtn;

    [Header("Needed Craft Items")]
    [SerializeField] private UICraftNeededItem neededItemPrefab;
    [SerializeField] private Transform neededItemsContent;

    [Header("Recipies")]
    [SerializeField] private TextMeshProUGUI placeText;

    [Space]
    [ReadOnly, SerializeField] private CraftInfo selectedCraftInfo;

    private ObjectPoolingManager poolingManager;

    private CraftInfo[] possibleCrafts;
    private AInventory targetInventory;

    private List<UICraftRecipe> spawnedRecipies;

    private List<UICraftNeededItem> spawnedNeededItems;

    public static UIPlayerCraftsManager Instance;

    private void OnDisable()
    {
        possibleCrafts = null;
        targetInventory = null;
        selectedCraftInfo = null;
        selectedCraftPanel.gameObject.SetActive(false);
    }

    public void OnInitialize()
    {
        Instance = this;

        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();

        foreach (var type in types)
        {
            type.OnInitialize();
            type.onTypeChange += ChangeType;
        }

        spawnedRecipies = new List<UICraftRecipe>();
        spawnedNeededItems = new List<UICraftNeededItem>();

        craftSelectedBtn.onClick.AddListener(() =>
        {
            CraftSelected();
        });
    }

    public void OnDeinitialize()
    {
        Instance = null;

        foreach (var type in types)
        {
            type.onTypeChange -= ChangeType;
        }
    }

    private void ChangeType(CraftType type)
    {
        foreach (UICraftRecipe spawned in spawnedRecipies)
        {
            spawned.onSelectClick -= OnSelectRecipe;
            spawned.OnDeinitialize();
            spawned.gameObject.SetActive(false);
        }

        spawnedRecipies.Clear();

        foreach (CraftInfo craft in possibleCrafts)
        {
            if (craft.Type != type) continue;

            UICraftRecipe newRecipe = poolingManager.GetPoolable(recipePrefab);
            newRecipe.onSelectClick += OnSelectRecipe;
            newRecipe.OnInitialize();
            newRecipe.transform.SetParent(recipiesContent);
            newRecipe.transform.localScale = Vector3.one;
            newRecipe.SetInfo(craft);
            spawnedRecipies.Add(newRecipe);
        }
    }

    private void CraftSelected()
    {
        targetInventory.TryCraft(selectedCraftInfo);
        UpdateSelectedRecipe();
    }

    private void OnSelectRecipe(CraftInfo info)
    {
        selectedCraftInfo = info;

        selectedCraftName.text = info.RecipeName;
        selectedCraftDesc.text = info.RecipeDesc;

        UpdateSelectedRecipe();

        selectedCraftPanel.SetActive(true);
    }

    private void UpdateSelectedRecipe()
    {
        foreach (UICraftNeededItem spawnedNItem in spawnedNeededItems)
        {
            spawnedNItem.gameObject.SetActive(false);
        }

        spawnedNeededItems.Clear();

        bool canCraft = true;

        foreach (ItemData neededItem in selectedCraftInfo.NeededItems)
        {
            UICraftNeededItem newNeededItem = poolingManager.GetPoolable(neededItemPrefab);
            newNeededItem.transform.SetParent(neededItemsContent);
            newNeededItem.transform.localScale = Vector3.one;

            if (!targetInventory.HasItem(neededItem, out int curAmount))
            {
                canCraft = false;
            }

            newNeededItem.SetData(neededItem.Info.CellIcon, curAmount, neededItem.RandomAmount);
            spawnedNeededItems.Add(newNeededItem);
        }

        craftSelectedBtn.interactable = canCraft;
    }

    public void Open(CraftInfo[] possibleCrafts, AInventory targetInventory, string placeName)
    {
        this.possibleCrafts = possibleCrafts;
        this.targetInventory = targetInventory;

        placeText.text = placeName;

        ChangeType(CraftType.Other);
    }
}
