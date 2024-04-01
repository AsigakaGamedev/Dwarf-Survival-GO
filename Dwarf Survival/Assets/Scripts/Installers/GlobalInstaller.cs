using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private LoadingManager loadingManager;

    public override void InstallBindings()
    {
        Container.Bind<LoadingManager>().FromInstance(loadingManager);
    }
}
