using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WorldObjectData
{
    public PoolableObject Prefab;

    [Space]
    public string BiomeID;
    public WorldCellType CellType;
    public bool NeedRotate;

    [Space]
    public int StartCount;
    public int MaxCount;
}
