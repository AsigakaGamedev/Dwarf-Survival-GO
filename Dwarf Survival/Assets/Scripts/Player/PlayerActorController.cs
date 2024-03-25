using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerActorController : MonoBehaviour
{
    [SerializeField] private Actor actor;

    private InputsManager inputs;
    private UIManager uiManager;
    private Camera mainCamera;
    private UIPlayerCraftsManager uiPlayerCrafts;

    public Actor Actor { get => actor; }

    public Action onDie;

    [Inject]
    public void Construct(InputsManager inputs, UIManager uiManager, UIPlayerCraftsManager uiPlayerCrafts)
    {
        this.inputs = inputs;

        inputs.onMove += OnMoveInput;
        inputs.onAttack += OnAttackInput;
        inputs.onInteract += OnInteractInput;
        inputs.onInventoryOpen += OnInventoryOpenInput;
        inputs.onPlayerCraftsOpen += OnPlayerCraftOpenInput;

        this.uiManager = uiManager;
        this.uiPlayerCrafts = uiPlayerCrafts;
    }

    private void Start()
    {
        actor.OnInitialize();
        actor.ChangeMovementType(true);

        actor.Health.onDie += OnDie;

        mainCamera = Camera.main;
    }

    private void Update()
    {
        actor.Interactions.CheckInteractions();

        if (actor.Needs) actor.Needs.UpdateNeeds();
    }

    private void OnDestroy()
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
        uiPlayerCrafts.Open(actor.Inventory.PossibleCrafts, actor.Inventory, "Player");
    }

    #endregion

    #region

    private void OnDie()
    {
        onDie?.Invoke();
    }

    #endregion
}
