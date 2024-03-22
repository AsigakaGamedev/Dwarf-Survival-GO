using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerCraftsManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private UICraftType[] types;

    [Space]
    [SerializeField] private UICraftRecipe recipePrefab;
    [SerializeField] private Transform recipiesContent;

    private ObjectPoolingManager poolingManager;

    private CraftInfo[] possibleCrafts;
    private AInventory resultInventory;

    private List<UICraftRecipe> spawnedRecipies;

    public static UIPlayerCraftsManager Instance;

    private void OnDisable()
    {
        possibleCrafts = null;
        resultInventory = null;
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
            spawned.gameObject.SetActive(false);
        }

        spawnedRecipies.Clear();

        foreach (CraftInfo craft in possibleCrafts)
        {
            if (craft.Type != type) continue;

            UICraftRecipe newRecipe = poolingManager.GetPoolable(recipePrefab);
            newRecipe.transform.SetParent(recipiesContent);
            newRecipe.transform.localScale = Vector3.one;
            newRecipe.SetInfo(craft);
            spawnedRecipies.Add(newRecipe);
        }
    }

    public void Open(CraftInfo[] possibleCrafts, AInventory resultInventory)
    {
        this.possibleCrafts = possibleCrafts;
        this.resultInventory = resultInventory;

        ChangeType(CraftType.Other);
    }
}
