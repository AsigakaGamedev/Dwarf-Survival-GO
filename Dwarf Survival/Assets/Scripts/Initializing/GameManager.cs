using AYellowpaper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> allListeners;

    private void Start()
    {
        foreach (var initializable in allListeners)
        {
            if (initializable is IInitListener init) init.OnInitialize();
        }
    }

    private void Update()
    {
        foreach (var initializable in allListeners)
        {
            if (initializable is IUpdateListener upd) upd.OnUpdate();
        }
    }

    private void OnDestroy()
    {
        foreach (var initializable in allListeners)
        {
            if (initializable is IDeinitListener deinit) deinit.OnDeinitialize();
        }
    }
}
