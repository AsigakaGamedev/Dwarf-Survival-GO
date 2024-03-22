using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerCraftsManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private UICraftType[] types;

    [Space]
    [SerializeField] private UICraftRecipe recipePrefab;
    [SerializeField] private Transform recipiesContent;

    [Header("Selected Craft")]
    [SerializeField] private GameObject selectedCraftPanel;
    [SerializeField] private TextMeshProUGUI selectedCraftName;
    [SerializeField] private TextMeshProUGUI selectedCraftDesc;

    [Space]
    [SerializeField] private Button craftSelectedBtn;

    [Space]
    [ReadOnly, SerializeField] private CraftInfo selectedCraftInfo;

    private ObjectPoolingManager poolingManager;

    private CraftInfo[] possibleCrafts;
    private AInventory resultInventory;

    private List<UICraftRecipe> spawnedRecipies;

    public static UIPlayerCraftsManager Instance;

    private void OnDisable()
    {
        possibleCrafts = null;
        resultInventory = null;
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
        print($"{selectedCraftInfo} crafted");
    }

    private void OnSelectRecipe(CraftInfo info)
    {
        selectedCraftInfo = info;

        selectedCraftName.text = info.RecipeName;
        selectedCraftDesc.text = info.RecipeDesc;

        selectedCraftPanel.SetActive(true);
    }

    public void Open(CraftInfo[] possibleCrafts, AInventory resultInventory)
    {
        this.possibleCrafts = possibleCrafts;
        this.resultInventory = resultInventory;

        ChangeType(CraftType.Other);
    }
}
