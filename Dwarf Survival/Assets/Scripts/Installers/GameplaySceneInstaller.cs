using Asigaka.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private InputsManager inputsManager;
    [SerializeField] private BuildingsManager buildingsManager;
    [SerializeField] private ObjectPoolingManager poolingManager;

    [Header("UI")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private UIPlayerCraftsManager uiPlayerCrafts;
    [SerializeField] private UIEffectsManager uiEffectsManager;
    [SerializeField] private UIInventoriesManager uiInventoriesManager;
    [SerializeField] private UIRecyclingManager uiRecyclingManager;

    [Header("Test")]
    [SerializeField] private bool bootstrapOnstart;

    public WorldManager WorldManager { get => worldManager; }
    public PlayerManager PlayerManager { get => playerManager; }

    public static GameplaySceneInstaller Instance { get; private set; }

    public override void InstallBindings()
    {
        Container.Bind<WorldManager>().FromInstance(worldManager).AsSingle();
        Container.Bind<PlayerManager>().FromInstance(playerManager).AsSingle();
        Container.Bind<CameraManager>().FromInstance(cameraManager).AsSingle();
        Container.Bind<InputsManager>().FromInstance(inputsManager).AsSingle();
        Container.Bind<BuildingsManager>().FromInstance(buildingsManager).AsSingle();
        Container.Bind<ObjectPoolingManager>().FromInstance(poolingManager).AsSingle();
        
        Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
        Container.Bind<UIPlayerCraftsManager>().FromInstance(uiPlayerCrafts).AsSingle();
        Container.Bind<UIEffectsManager>().FromInstance(uiEffectsManager).AsSingle();
        Container.Bind<UIInventoriesManager>().FromInstance(uiInventoriesManager).AsSingle();
        Container.Bind<UIRecyclingManager>().FromInstance(uiRecyclingManager).AsSingle();

        Instance = this;
    }

    public override void Start()
    {
        base.Start();

        if (bootstrapOnstart)
        {
            worldManager.GenerateWorld();
            worldManager.GenerateObjects();
            playerManager.FirstSpawnPlayer();
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
