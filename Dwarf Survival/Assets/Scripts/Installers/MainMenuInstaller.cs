using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private ObjectPoolingManager poolingManager;

    [Header("UI")]
    [SerializeField] private UIManager uiManager;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPoolingManager>().FromInstance(poolingManager);
        poolingManager.Init();

        Container.Bind<UIManager>().FromInstance(uiManager);
    }
}
