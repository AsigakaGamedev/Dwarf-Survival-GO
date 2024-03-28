using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseSpawnableObject 
{
    [SerializeField] private PoolableObject prefab;
    [SerializeField] private int maxCount;
    [SerializeField] private float productionCost;

    [HideInInspector] public int SpawnedCount;

    public PoolableObject Prefab { get => prefab; }
    public int MaxCount { get => maxCount; }
    public float ProductionCost { get => productionCost; }
}
