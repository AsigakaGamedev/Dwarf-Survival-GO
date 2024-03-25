using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerActorController playerPrefab;
    [SerializeField] private string playerSpawnBiome = "cave";

    [Space]
    [ReadOnly, SerializeField] private PlayerActorController playerInstance;

    [Space]
    [ReadOnly, SerializeField] private Vector2 spawnPoint;

    private WorldManager worldManager;
    private CameraManager cameraManager;
    private DiContainer diContainer;

    public Action<PlayerActorController> onPlayerSpawn;

    public PlayerActorController PlayerInstance { get => playerInstance; }

    [Inject]
    private void Construct(WorldManager worldManager, CameraManager cameraManager, DiContainer diContainer)
    {
        this.worldManager = worldManager;
        this.cameraManager = cameraManager;
        this.diContainer = diContainer;
    }

    private void Start()
    {
        WorldCellData spawnCell = worldManager.GetRandomCell(playerSpawnBiome, WorldCellType.Ground);
        spawnPoint = new Vector2(spawnCell.PosX + 0.5f, spawnCell.PosY + 0.5f);

        SpawnPlayer();

        print("Player Manager инициализирован");
    }

    private void OnDestroy()
    {
        if (!playerInstance) return;

        playerInstance.onDie -= OnPlayerDie;
    }

    private void SpawnPlayer()
    {
        if (!playerInstance)
        {
            playerInstance = diContainer.InstantiatePrefabForComponent<PlayerActorController>(playerPrefab, spawnPoint, playerPrefab.transform.rotation, transform);
            cameraManager.SetCameraTarget(playerInstance.transform);
            playerInstance.onDie += OnPlayerDie;
            onPlayerSpawn?.Invoke(playerInstance);
        }
        else
        {
            playerInstance.transform.position = spawnPoint;
            playerInstance.gameObject.SetActive(true);
        }
    }

    private void OnPlayerDie()
    {
        StartCoroutine(ERespawnPlayer());
    }

    private IEnumerator ERespawnPlayer()
    {
        yield return new WaitForSeconds(4);

        SpawnPlayer();
        playerInstance.Revive();
    }
}
