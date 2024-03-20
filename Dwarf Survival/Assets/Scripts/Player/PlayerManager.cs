using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private PlayerActorController playerPrefab;
    [SerializeField] private string playerSpawnBiome = "cave";

    [Space]
    [ReadOnly, SerializeField] private PlayerActorController playerInstance;

    private WorldManager worldManager;
    private CameraManager cameraManager;
    private InputsManager inputsManager;
    private UIManager uiManager;

    public PlayerActorController PlayerInstance { get => playerInstance; }

    public void OnInitialize()
    {
        worldManager = WorldManager.Instance;
        cameraManager = CameraManager.Instance;
        inputsManager = InputsManager.Instance;
        uiManager = ServiceLocator.GetService<UIManager>();

        WorldCellData spawnCell = worldManager.GetRandomCell(playerSpawnBiome, WorldCellType.Ground);
        playerInstance = Instantiate(playerPrefab, new Vector2(spawnCell.PosX, spawnCell.PosY), Quaternion.identity, transform);
        playerInstance.OnInitialize();
        cameraManager.SetCameraTarget(playerInstance.transform);

        inputsManager.onInventoryOpen += OnInventoryOpenClick;

        print("Player Manager инициализирован");
    }

    public void OnDeinitialize()
    {
        inputsManager.onInventoryOpen -= OnInventoryOpenClick;

        playerInstance.OnDeinitialize();
    }

    private void OnInventoryOpenClick()
    {
        uiManager.ChangeScreen("inventory");
    }
}
