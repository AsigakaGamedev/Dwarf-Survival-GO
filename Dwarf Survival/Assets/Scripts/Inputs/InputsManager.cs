using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputsManager : MonoBehaviour
{
    public Action<Vector2> onMove;
    public Action<Vector2> onLook;

    public Action onAttack;

    public Action onInventoryOpen;
    public Action onInteract;
    public Action onPlayerCraftsOpen;
}
