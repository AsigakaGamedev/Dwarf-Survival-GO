using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct WorldBiomeInfoData 
{
    public string ID;

    [Space]
    public TileBase GroundTile;
    public TileBase WallsTile;

    [Space]
    public float MinBiomeHeight;
    public float MaxBiomeHeight;

    [Space]
    public WorldBiomePointData[] Points;
}

[System.Serializable]
public struct WorldBiomePointData
{
    public float TargetHeight;
    public WorldCellType CellType;
}
