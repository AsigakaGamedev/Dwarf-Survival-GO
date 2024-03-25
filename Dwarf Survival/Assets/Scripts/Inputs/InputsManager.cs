using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { Desktop, Mobile }

public class InputsManager : MonoBehaviour
{
    [SerializeField] private InputType inputType;

    public Action<Vector2> onMove;
    public Action<Vector2> onLook;

    public Action onAttack;

    public Action onInventoryOpen;
    public Action onInteract;
    public Action onPlayerCraftsOpen;

    private void Update()
    {
        onMove?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetMouseButton(0)) onAttack?.Invoke();

        if (Input.GetKeyDown(KeyCode.I)) onInventoryOpen?.Invoke();
        if (Input.GetKeyDown(KeyCode.F)) onInteract?.Invoke();
        if (Input.GetKeyDown(KeyCode.C)) onPlayerCraftsOpen?.Invoke();
    }
}
