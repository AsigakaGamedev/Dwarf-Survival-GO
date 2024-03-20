using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActorController : MonoBehaviour, IInitListener, IUpdateListener, IDeinitListener
{
    [SerializeField] private Actor actor;

    [Space]
    [SerializeField] private InteractionsController interactions;

    private InputsManager inputs;

    public void OnInitialize()
    {
        actor.OnInitialize();
        actor.ChangeMovementType(true);

        inputs = InputsManager.Instance;

        inputs.onMove += OnMove;

        print("Player Actor Controller инициализирован");
    }

    public void OnUpdate()
    {
        interactions.CheckInteractions();
    }

    public void OnDeinitialize()
    {
        actor.OnDeinitialize();

        inputs.onMove -= OnMove;
    }

    private void OnMove(Vector2 dir)
    {
        actor.MoveByDir(dir);
    }
}
