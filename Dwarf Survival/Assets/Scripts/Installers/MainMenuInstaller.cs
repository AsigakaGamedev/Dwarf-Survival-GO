using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [Header("UI")]
    [SerializeField] private UIManager uiManager;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromInstance(uiManager);
    }
}
