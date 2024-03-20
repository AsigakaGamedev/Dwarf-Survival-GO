using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum WorldCellType { Ground, Wall }

public class WorldManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [Expandable, SerializeField] private WorldPreset currentPreset;

    [Space]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap wallsTilemap;

    private WorldCellData[,] worldCells;
    private Dictionary<string, WorldBiomeData> biomes;

    private ObjectPoolingManager poolingManager;

    public static WorldManager Instance;

    public void OnInitialize()
    {
        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();

        Instance = this;

        GenerateCurrent();

        print("World Manager инициализирован");
    }

    public void OnDeinitialize()
    {
        Instance = null;
    }

    [Button]
    public void GenerateCurrent()
    {
        print("Генерация тайлов...");

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
                float xCoord = (float)x / currentPreset.Size * currentPreset.Scale;
                float yCoord = (float)y / currentPreset.Size * currentPreset.Scale;

                float height = Mathf.PerlinNoise(xCoord, yCoord);

                WorldBiomeData biome = biomes[GetBiomeID(height, out WorldCellType cellType)];

                if (cellType == WorldCellType.Ground)
                {
                    groundTilemap.SetTile(new Vector3Int(x, y), biome.Info.GroundTile);
                }
                else if (cellType == WorldCellType.Wall)
                {
                    wallsTilemap.SetTile(new Vector3Int(x, y), biome.Info.WallsTile);
                }

                worldCells[x, y] = new WorldCellData(x, y, cellType);
                biome.Cells.Add(worldCells[x, y]);
            }
        }

        print("Генерация объектов...");

        foreach (WorldObjectData worldObject in currentPreset.Objects)
        {
            for (int i = 0; i < worldObject.StartCount; i++)
            {
                PoolableObject newObject = poolingManager.GetPoolable(worldObject.Prefab, worldObject.StartCount);
                //newObject.transform.parent = transform;
                
                if (worldObject.NeedRotate) newObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

                WorldCellData randomCell = GetRandomCell(worldObject.BiomeID, worldObject.CellType);
                newObject.transform.position = new Vector2(randomCell.PosX, randomCell.PosY);
            }
        }

        print("Мир сгенерировался");
    }

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

        throw new System.Exception($"Для высоты {height} не нашёлся биом");
    }

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
}

public struct WorldCellData
{
    public readonly int PosX;
    public readonly int PosY;

    public WorldCellType CellType;

    public WorldCellData(int posX, int posY, WorldCellType cellType)
    {
        PosX = posX;
        PosY = posY;
        CellType = cellType;
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
