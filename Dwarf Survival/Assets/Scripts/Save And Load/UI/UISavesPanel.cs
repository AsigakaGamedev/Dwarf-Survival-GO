using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UISavesPanel : MonoBehaviour
{
    [SerializeField] private UISaveListItem saveItemPrefab;
    [SerializeField] private Transform savesContent;

    [Header("Selected Save")]
    [SerializeField] private GameObject selectedSavePanel;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button deleteBtn;

    private SavesManager savesManager;
    private ObjectPoolingManager poolingManager;

    private List<UISaveListItem> spawnedSaves;
    private SaveEntity selectedSave;

    [Inject]
    private void Construct(SavesManager savesManager, ObjectPoolingManager poolingManager)
    {
        this.savesManager = savesManager;
        this.poolingManager = poolingManager;

        spawnedSaves = new List<UISaveListItem>();
    }

    private void OnEnable()
    {
        UpdatePanel();
    }

    private void Awake()
    {
        if (loadBtn) loadBtn.onClick.AddListener(LoadSelectedSave);
        if (saveBtn) saveBtn.onClick.AddListener(ResaveSelectedSave);
        deleteBtn.onClick.AddListener(DeleteSelectedSave);

        savesManager.onSavesUpdate += UpdatePanel;
    }

    private void OnDestroy()
    {
        savesManager.onSavesUpdate -= UpdatePanel;
    }

    private void UpdatePanel()
    {
        foreach (UISaveListItem item in spawnedSaves)
        {
            item.gameObject.SetActive(false);
            item.onSaveSelect -= OnSaveSelect;
        }

        spawnedSaves.Clear();

        selectedSave = null;
        selectedSavePanel.SetActive(false);

        foreach (SaveEntity saveEntity in savesManager.AllSaves)
        {
            UISaveListItem newSaveItem = poolingManager.GetPoolable(saveItemPrefab);
            newSaveItem.transform.SetParent(savesContent);
            newSaveItem.transform.localScale = Vector3.one;
            newSaveItem.SetEntity(saveEntity);
            newSaveItem.onSaveSelect += OnSaveSelect;
            spawnedSaves.Add(newSaveItem);
        }
    }

    private void OnSaveSelect(SaveEntity saveEntity)
    {
        selectedSavePanel.SetActive(true);
        selectedSave = saveEntity;
    }

    private void LoadSelectedSave()
    {
        if (selectedSave == null) return;

        savesManager.LoadSave(selectedSave.Name);
    }

    private void ResaveSelectedSave()
    {
        if (selectedSave == null) return;

        savesManager.CreateSave(selectedSave.Name);
    }

    private void DeleteSelectedSave()
    {
        if (selectedSave == null) return;

        savesManager.DeleteSave(selectedSave.Name);

        UpdatePanel();
    }
}
