using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectSpawnData
{
    [SerializeField] private PoolableObject prefab; 
    [SerializeField] private int spawnAmount; 
    [SerializeField] private float spawnChance;

    public PoolableObject Prefab { get => prefab; }
    public int SpawnAmount { get => spawnAmount; }

    public bool CanSpawn()
    {
        float curChange = Random.Range(0, 100);
        return curChange >= spawnChance;
    }
}
