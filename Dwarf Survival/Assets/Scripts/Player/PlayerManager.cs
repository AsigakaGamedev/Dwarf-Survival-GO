using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IInitListener, IUpdateListener, IDeinitListener
{
    [SerializeField] private PlayerActorController playerPrefab;
    [SerializeField] private string playerSpawnBiome = "cave";

    [Space]
    [ReadOnly, SerializeField] private PlayerActorController playerInstance;

    [Space]
    [ReadOnly, SerializeField] private Vector2 spawnPoint;

    private WorldManager worldManager;
    private CameraManager cameraManager;

    public PlayerActorController PlayerInstance { get => playerInstance; }

    public static PlayerManager Instance;

    public void OnInitialize()
    {
        Instance = this;

        worldManager = WorldManager.Instance;
        cameraManager = CameraManager.Instance;

        WorldCellData spawnCell = worldManager.GetRandomCell(playerSpawnBiome, WorldCellType.Ground);
        spawnPoint = new Vector2(spawnCell.PosX + 0.5f, spawnCell.PosY + 0.5f);

        SpawnPlayer();

        print("Player Manager инициализирован");
    }

    public void OnUpdate()
    {
        playerInstance.OnUpdate();
    }

    public void OnDeinitialize()
    {
        Instance = null;

        playerInstance.OnDeinitialize();
        playerInstance.onDie -= OnPlayerDie;
    }

    private void SpawnPlayer()
    {
        if (!playerInstance)
        {
            playerInstance = Instantiate(playerPrefab, spawnPoint, playerPrefab.transform.rotation, transform);
            playerInstance.OnInitialize();
            cameraManager.SetCameraTarget(playerInstance.transform);
            playerInstance.onDie += OnPlayerDie;
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
