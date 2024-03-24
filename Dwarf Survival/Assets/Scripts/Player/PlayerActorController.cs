using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActorController : MonoBehaviour, IInitListener, IUpdateListener, IDeinitListener
{
    [SerializeField] private Actor actor;

    private InputsManager inputs;
    private UIManager uiManager;
    private Camera mainCamera;

    public Actor Actor { get => actor; }

    public Action onDie;

    public void OnInitialize()
    {
        actor.OnInitialize();
        actor.ChangeMovementType(true);

        actor.Health.onDie += OnDie;

        inputs = InputsManager.Instance;
        uiManager = ServiceLocator.GetService<UIManager>();
        mainCamera = Camera.main;

        inputs.onMove += OnMoveInput;
        inputs.onAttack += OnAttackInput;
        inputs.onInteract += OnInteractInput;
        inputs.onInventoryOpen += OnInventoryOpenInput;
        inputs.onPlayerCraftsOpen += OnPlayerCraftOpenInput;

        print("Player Actor Controller инициализирован");
    }

    public void OnUpdate()
    {
        actor.Interactions.CheckInteractions();

        if (actor.Needs) actor.Needs.UpdateNeeds();
    }

    public void OnDeinitialize()
    {
        actor.OnDeinitialize();

        actor.Health.onDie -= OnDie;

        inputs.onMove -= OnMoveInput;
        inputs.onAttack -= OnAttackInput;
        inputs.onInteract -= OnInteractInput;
        inputs.onInventoryOpen -= OnInventoryOpenInput;
        inputs.onPlayerCraftsOpen -= OnPlayerCraftOpenInput;
    }

    public void Revive()
    {
        actor.Revive();
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

    private void OnAttackInput()
    {
        Vector2 attackDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        attackDir.Normalize();
        actor.Attack(attackDir);
    }

    private void OnInventoryOpenInput()
    {
        uiManager.ChangeScreen("inventory");
    }

    private void OnPlayerCraftOpenInput()
    {
        uiManager.ChangeScreen("craft");
        UIPlayerCraftsManager.Instance.Open(actor.Inventory.PossibleCrafts, actor.Inventory, "Player");
    }

    #endregion

    #region

    private void OnDie()
    {
        onDie?.Invoke();
    }

    #endregion
}
