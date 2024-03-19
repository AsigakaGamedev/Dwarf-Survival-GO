using AYellowpaper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IInitializable, MonoBehaviour>[] initializables;

    private void Start()
    {
        foreach (var initializable in initializables)
        {
            initializable.Value.OnInitialize();
        }
    }

    private void OnDestroy()
    {
        foreach (var initializable in initializables)
        {
            initializable.Value.OnDeinitialize();
        }
    }
}
