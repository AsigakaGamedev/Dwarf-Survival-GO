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

    public void OnInitialize()
    {
        actor.OnInitialize();
        actor.ChangeMovementType(true);

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
    }

    public void OnDeinitialize()
    {
        actor.OnDeinitialize();

        inputs.onMove -= OnMoveInput;
        inputs.onAttack -= OnAttackInput;
        inputs.onInteract -= OnInteractInput;
        inputs.onInventoryOpen -= OnInventoryOpenInput;
        inputs.onPlayerCraftsOpen -= OnPlayerCraftOpenInput;
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
        UIPlayerCraftsManager.Instance.Open(actor.Inventory.PossibleCrafts, actor.Inventory);
    }

    #endregion
}
