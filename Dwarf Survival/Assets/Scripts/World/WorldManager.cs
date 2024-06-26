using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public enum WorldCellType { Ground, Wall }

public class WorldManager : MonoBehaviour
{
    [Expandable, SerializeField] private WorldPreset currentPreset;

    [Space]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap wallsTilemap;

    [Space]
    [SerializeField] private TilemapCollider2D wallsTilemapCollider; 

    private int currentSeed;
    private WorldCellData[,] worldCells;
    private Dictionary<string, WorldBiomeData> biomes;

    private ObjectPoolingManager poolingManager;
    private AStarPathfinding pathfinding;

    public WorldCellData[,] WorldCells { get => worldCells; }

    [Inject]
    public void Construct(ObjectPoolingManager poolingManager, AStarPathfinding pathfinding)
    {
        this.poolingManager = poolingManager;
        this.pathfinding = pathfinding;
    }

    #region Generating

    public void GenerateNewWorld(int seed)
    {
        groundTilemap.ClearAllTiles();
        wallsTilemap.ClearAllTiles();
        print("��������� ������...");

        currentSeed = seed;

        worldCells = new WorldCellData[currentPreset.Size, currentPreset.Size];
        biomes = new Dictionary<string, WorldBiomeData>();

        foreach (WorldBiomeInfoData biomeInfo in currentPreset.Biomes)
        {
            biomes.Add(biomeInfo.ID, new WorldBiomeData(biomeInfo));
        }

        for (int x = 0; x < currentPreset.Size; x++)
        {
            for (int y = 0; y < currentPreset.Size; y++)
            {
                float xCoord = (float)x / currentPreset.Size * currentPreset.Scale + seed;
                float yCoord = (float)y / currentPreset.Size * currentPreset.Scale + seed;

                float height = Mathf.PerlinNoise(xCoord, yCoord);

                string biomeID = GetBiomeID(height, out WorldCellType cellType);
                WorldBiomeData biome = biomes[biomeID];

                if (cellType == WorldCellType.Ground)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y), biome.Info.GroundTile);
                }
                else if (cellType == WorldCellType.Wall)
                {
                    wallsTilemap.SetTile(new Vector3Int(x, y), biome.Info.WallsTile);
                }

                worldCells[x, y] = new WorldCellData(biomeID, x, y, cellType, 3);
                biome.Cells.Add(worldCells[x, y]);
            }
        }

        pathfinding.SetCells(worldCells);
    }

    public void GenerateWorld(WorldSaveData saveData)
    {
        groundTilemap.ClearAllTiles();
        wallsTilemap.ClearAllTiles();

        currentSeed = saveData.Seed;
        worldCells = saveData.Cells;

        biomes = new Dictionary<string, WorldBiomeData>();

        foreach (WorldBiomeInfoData biomeInfo in currentPreset.Biomes)
        {
            biomes.Add(biomeInfo.ID, new WorldBiomeData(biomeInfo));
        }

        for (int x = 0; x < currentPreset.Size; x++)
        {
            for (int y = 0; y < currentPreset.Size; y++)
            {
                WorldCellData cell = worldCells[x, y];
                WorldBiomeData biome = biomes[cell.BiomeID];

                if (cell.CellType == WorldCellType.Ground)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y), biome.Info.GroundTile);
                }
                else if (cell.CellType == WorldCellType.Wall)
                {
                    wallsTilemap.SetTile(new Vector3Int(x, y), biome.Info.WallsTile);
                }

                biome.Cells.Add(worldCells[x, y]);
            }
        }

        pathfinding.SetCells(worldCells);
    }

    public void GenerateNewObjects()
    {
        print("��������� ��������...");

        foreach (WorldObjectData worldObject in currentPreset.Objects)
        {
            for (int i = 0; i < worldObject.StartCount; i++)
            {
                PoolableObject newObject = poolingManager.GetPoolable(worldObject.Prefab, worldObject.StartCount);

                if (worldObject.NeedRotate) newObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

                WorldCellData randomCell = GetRandomCell(worldObject.BiomeID, worldObject.CellType);
                newObject.transform.position = new Vector2(randomCell.PosX, randomCell.PosY) + Vector2.one / 2;
            }
        }
    }

    #endregion

    #region Save And Loading

    public WorldSaveData SaveWorld()
    {
        return new WorldSaveData()
        {
            Seed = currentSeed,
            Cells = worldCells
        };
    }

    #endregion

    #region Biomes

    public string GetBiomeID(float height, out WorldCellType cellType)
    {
        foreach (WorldBiomeData biome in biomes.Values)
        {
            if (height >= biome.Info.MinBiomeHeight && height <= biome.Info.MaxBiomeHeight)
            {
                foreach (WorldBiomePointData point in biome.Info.Points)
                {
                    if (height <= point.TargetHeight)
                    {
                        cellType = point.CellType;
                        return biome.Info.ID;
                    }
                }

                cellType = WorldCellType.Wall;
                return biome.Info.ID;
            }
        }

        cellType = WorldCellType.Ground;
        return "cave";
        //throw new System.Exception($"��� ������ {height} �� ������� ����");
    }

    #endregion

    #region Cells

    public WorldCellData GetRandomCell(string biomeID, WorldCellType cellType)
    {
        WorldCellData randomCell = biomes[biomeID].Cells[Random.Range(0, biomes[biomeID].Cells.Count)];

        for (int i = 0; i < 25; i++)
        {
            randomCell = biomes[biomeID].Cells[Random.Range(0, biomes[biomeID].Cells.Count)];

            if (randomCell.CellType == cellType)
            {
                break;
            }
        }

        return randomCell;
    }

    public WorldCellData GetCell(Vector2 rawPos)
    {
        return worldCells[(int)rawPos.x, (int)rawPos.y];
    }

    public bool TryMineCell(Vector2 minePoint, float miningPower)
    {
        WorldCellData miningCell = GetCell(minePoint);

        if (miningCell.Health == -1) return false;

        miningCell.Health -= miningPower;
        //print($"{miningCell.PosX} {miningCell.PosY} : {miningCell.Health}");

        if (miningCell.Health <= 0)
        {
            Vector3Int tilePos = new Vector3Int(miningCell.PosX, miningCell.PosY, 0);

            wallsTilemapCollider.enabled = false;
            miningCell.CellType = WorldCellType.Ground;
            groundTilemap.SetTile(tilePos, biomes[miningCell.BiomeID].Info.GroundTile);
            wallsTilemap.SetTile(tilePos, null);
            wallsTilemapCollider.enabled = true;

            foreach (ObjectSpawnData mineResult in biomes[miningCell.BiomeID].Info.MiningResultObjects)
            {
                for (int i = 0; i < mineResult.SpawnAmount; i++)
                {
                    if (!mineResult.CanSpawn()) continue;

                    for (int j = 0; j < mineResult.SpawnAmount; j++)
                    {
                        PoolableObject spawnedResult = poolingManager.GetPoolable(mineResult.Prefab);
                        spawnedResult.transform.position = (Vector3)tilePos + Vector3.one / 2;
                    }
                }
            }
        }

        return true;
    }

    #endregion
}

public class WorldCellData
{
    public string BiomeID;

    public int PosX;
    public int PosY;

    public WorldCellType CellType;
    public float Health;

    public int Cost { get; set; }

    public WorldCellData(string biomeID, int posX, int posY, WorldCellType cellType, float health)
    {
        BiomeID = biomeID;
        PosX = posX;
        PosY = posY;
        CellType = cellType;
        Health = health;
        Cost = int.MaxValue;
    }
}

public struct WorldBiomeData
{
    public readonly WorldBiomeInfoData Info;

    public List<WorldCellData> Cells;

    public WorldBiomeData(WorldBiomeInfoData info) : this()
    {
        Info = info;
        Cells = new List<WorldCellData>();
    }
}
