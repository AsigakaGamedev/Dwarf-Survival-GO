using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileInputsManager : InputsManager
{
    [SerializeField] private Joystick moveJoystick;

    [Space]
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button attackBtn;

    private void Update()
    {
        onMove?.Invoke(new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical));
    }
}
