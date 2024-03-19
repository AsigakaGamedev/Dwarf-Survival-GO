using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { Desktop, Mobile }

public class InputsManager : MonoBehaviour, IInitializable
{
    [SerializeField] private InputType inputType;

    public Action<Vector3> onMove;

    public Action onStartAttackPrepare;
    public Action onEndAttackPrepare;

    public static InputsManager Instance;

    public void OnInitialize()
    {
        print("Inputs Manager Инициализирован");

        Instance = this;

        if (inputType == InputType.Desktop)
        {
            //StartCoroutine(EUpdateDesktopInputs());
        }
    }

    public void OnDeinitialize()
    {

    }

    private void Update()
    {
        onMove?.Invoke(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        if (Input.GetMouseButtonDown(0)) onStartAttackPrepare?.Invoke();
        else if (Input.GetMouseButtonUp(0)) onEndAttackPrepare?.Invoke();
    }
}
