using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInputsManager : InputsManager
{
    private void Update()
    {
        onMove?.Invoke(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        if (Input.GetMouseButton(0)) onAttack?.Invoke();

        if (Input.GetKeyDown(KeyCode.I)) onInventoryOpen?.Invoke();
        if (Input.GetKeyDown(KeyCode.F)) onInteract?.Invoke();
        if (Input.GetKeyDown(KeyCode.C)) onPlayerCraftsOpen?.Invoke();
    }
}
