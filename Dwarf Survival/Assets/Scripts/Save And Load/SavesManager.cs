using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SavesManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private List<SaveEntity> allSaves;

    [Space]
    [Scene, SerializeField] private string gameplaySceneName;

    private LoadingManager loadingManager;

    private string savesPath => $"{Application.dataPath}/Saves/";

    public Action onSavesUpdate;

    public List<SaveEntity> AllSaves { get => allSaves; }

    [Inject]
    private void Construct(LoadingManager loadingManager)
    {
        this.loadingManager = loadingManager;
    }

    private void Awake()
    {
        print($"Saves Path is - {savesPath}");
        CheckAndLoadSaves();
    }

    [Button]
    public void CheckAndLoadSaves()
    {
        allSaves = new List<SaveEntity>();

        foreach (string fileName in ES3.GetFiles(savesPath))
        {
            if (!ES3.FileExists(GetSavePath(fileName))) continue;

            try
            {
                SaveEntity loadedSave = ES3.Load<SaveEntity>(fileName, GetSavePath(fileName));
                allSaves.Add(loadedSave);
            }
            catch { }
        }

        onSavesUpdate?.Invoke();
    }

    public void CreateSave(string saveName)
    {
        WorldSaveData worldSaveData = GameplaySceneInstaller.Instance.WorldManager.SaveWorld();

        SaveEntity newSave = new SaveEntity(saveName, GetSavePath(saveName), worldSaveData);
        ES3.Save(saveName, newSave, GetSavePath(saveName));
        CheckAndLoadSaves();
    }

    public void DeleteSave(string saveName)
    {
        ES3.DeleteFile(GetSavePath(saveName));
        CheckAndLoadSaves();
    }

    public void LoadSave(string fileName)
    {
        SaveEntity loadedSave = ES3.Load<SaveEntity>(fileName, GetSavePath(fileName));

        LoadingAction[] startLoadingActions = new LoadingAction[0];

        LoadingAction loadWorldAction = new LoadingAction("World Loading", () =>
        {
            WorldManager worldManager = GameplaySceneInstaller.Instance.WorldManager;
            worldManager.GenerateWorld(loadedSave.WorldData);
        });

        LoadingAction loadObjectsAction = new LoadingAction("Objects Loading", () =>
        {
            WorldManager worldManager = GameplaySceneInstaller.Instance.WorldManager;
            worldManager.GenerateNewObjects();
        });

        LoadingAction loadPlayerAction = new LoadingAction("Player Loading", () =>
        {
            PlayerManager playerManager = GameplaySceneInstaller.Instance.PlayerManager;
            playerManager.FirstSpawnPlayer();
        });

        LoadingAction[] finishLoadingActions = new LoadingAction[]
        {
            loadWorldAction,
            loadObjectsAction,
            loadPlayerAction
        };

        loadingManager.LoadSceneAsync(gameplaySceneName, startLoadingActions, finishLoadingActions);
    }

    private string GetSavePath(string saveName) => $"{savesPath}{saveName}";
}

[System.Serializable]
public class SaveEntity
{
    public string Name;
    public string Path;

    public WorldSaveData WorldData;

    public SaveEntity(string name, string path, WorldSaveData worldData)
    {
        Name = name;
        Path = path;
        WorldData = worldData;
    }
}
