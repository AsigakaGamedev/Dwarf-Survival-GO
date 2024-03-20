using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { Desktop, Mobile }

public class InputsManager : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private InputType inputType;

    public Action<Vector2> onMove;

    public Action onAttack;

    public Action onInventoryOpen;

    public static InputsManager Instance;

    public void OnInitialize()
    {
        print("Inputs Manager инициализирован");

        Instance = this;

        if (inputType == InputType.Desktop)
        {
            //StartCoroutine(EUpdateDesktopInputs());
        }
    }

    public void OnDeinitialize()
    {
        Instance = null;
    }

    private void Update()
    {
        onMove?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetMouseButton(0)) onAttack?.Invoke();

        if (Input.GetKeyDown(KeyCode.I)) onInventoryOpen?.Invoke();
    }
}
