using Asigaka.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private InputsManager inputsManager;
    [SerializeField] private BuildingsManager buildingsManager;
    [SerializeField] private ObjectPoolingManager poolingManager;
    [SerializeField] private UIPlayerCraftsManager uiPlayerCrafts;
    [SerializeField] private UIEffectsManager uiEffectsManager;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromInstance(uiManager).AsSingle();
        Container.Bind<WorldManager>().FromInstance(worldManager).AsSingle();
        Container.Bind<PlayerManager>().FromInstance(playerManager).AsSingle();
        Container.Bind<CameraManager>().FromInstance(cameraManager).AsSingle();
        Container.Bind<InputsManager>().FromInstance(inputsManager).AsSingle();
        Container.Bind<BuildingsManager>().FromInstance(buildingsManager).AsSingle();
        Container.Bind<ObjectPoolingManager>().FromInstance(poolingManager).AsSingle();
        Container.Bind<UIPlayerCraftsManager>().FromInstance(uiPlayerCrafts).AsSingle();
        Container.Bind<UIEffectsManager>().FromInstance(uiEffectsManager).AsSingle();
    }
}
