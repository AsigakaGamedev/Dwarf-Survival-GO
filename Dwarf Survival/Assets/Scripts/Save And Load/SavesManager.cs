using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private List<SaveEntity> allSaves;

    private string savesPath => $"{Application.dataPath}/Saves/";

    public List<SaveEntity> AllSaves { get => allSaves; }

    private void Awake()
    {
        print($"Saves Path is - {savesPath}");
        CheckAndLoadSaves();
    }

    [Button]
    public void CreateTestEmptySave()
    {
        string saveName = $"Test Save {allSaves.Count}";
        SaveEntity newSave = new SaveEntity(saveName, GetSavePath(saveName));
        ES3.Save(saveName, newSave, GetSavePath(saveName));

        CheckAndLoadSaves();
    }

    [Button]
    public void CheckAndLoadSaves()
    {
        allSaves = new List<SaveEntity>();

        foreach (string fileName in ES3.GetFiles(savesPath))
        {
            if (!ES3.FileExists(GetSavePath(fileName))) continue;

            SaveEntity loadedSave = ES3.Load(fileName, GetSavePath(fileName)) as SaveEntity;
            allSaves.Add(loadedSave);
        }
    }

    public void DeleteSave(string saveName)
    {
        ES3.DeleteFile(GetSavePath(saveName));
        CheckAndLoadSaves();
    }

    private string GetSavePath(string saveName) => $"{savesPath}{saveName}";
}

[System.Serializable]
public class SaveEntity
{
    public string Name;
    public string Path;

    public SaveEntity(string name, string path)
    {
        Name = name;
        Path = path;
    }
}
