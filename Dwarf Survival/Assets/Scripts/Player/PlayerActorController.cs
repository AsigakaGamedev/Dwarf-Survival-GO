using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActorController : MonoBehaviour, IInitListener, IUpdateListener, IDeinitListener
{
    [SerializeField] private Actor actor;

    private InputsManager inputs;
    private UIManager uiManager;

    public void OnInitialize()
    {
        actor.OnInitialize();
        actor.ChangeMovementType(true);

        inputs = InputsManager.Instance;
        uiManager = ServiceLocator.GetService<UIManager>();

        inputs.onMove += OnMoveInput;
        inputs.onInteract += OnInteractInput;
        inputs.onInventoryOpen += OnInventoryOpenInput;

        print("Player Actor Controller инициализирован");
    }

    public void OnUpdate()
    {
        actor.Interactions.CheckInteractions();
    }

    public void OnDeinitialize()
    {
        actor.OnDeinitialize();

        inputs.onMove -= OnMoveInput;
        inputs.onInteract -= OnInteractInput;
        inputs.onInventoryOpen -= OnInventoryOpenInput;
    }

    #region Inputs

    private void OnMoveInput(Vector2 dir)
    {
        actor.MoveByDir(dir);
    }

    private void OnInteractInput()
    {
        actor.TryInteract();
    }

    private void OnInventoryOpenInput()
    {
        uiManager.ChangeScreen("inventory");
    }

    #endregion
}
