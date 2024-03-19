using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActorController : MonoBehaviour, IInitializable
{
    [SerializeField] private Actor actor;

    private InputsManager inputs;

    public void OnInitialize()
    {
        inputs = InputsManager.Instance;

        inputs.onMove += OnMove;
    }

    public void OnDeinitialize()
    {
        inputs.onMove -= OnMove;
    }

    private void OnMove(Vector2 dir)
    {
        actor.Move(dir);
    }
}
