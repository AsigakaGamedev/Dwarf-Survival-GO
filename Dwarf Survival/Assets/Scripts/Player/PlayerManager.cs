using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerActorController playerPrefab;
    [SerializeField] private string playerSpawnBiome = "cave";

    [Space]
    [ReadOnly, SerializeField] private PlayerActorController playerInstance;

    private WorldManager worldManager;

    public PlayerActorController PlayerInstance { get => playerInstance; }

    public void OnInitialize()
    {
        worldManager = WorldManager.Instance;

        WorldCellData spawnCell = worldManager.GetRandomCell(playerSpawnBiome, WorldCellType.Ground);
        playerInstance = Instantiate(playerPrefab, new Vector2(spawnCell.PosX, spawnCell.PosY), Quaternion.identity, transform);
        playerInstance.OnInitialize();

        print("Player Manager инициализирован");
    }

    public void OnDeinitialize()
    {
        playerInstance.OnDeinitialize();
    }
}
