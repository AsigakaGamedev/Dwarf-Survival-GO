using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MobileInputsManager : InputsManager
{
    [SerializeField] private Joystick moveJoystick;

    [Space]
    [SerializeField] private Button interactBtn;
    [SerializeField] private Button attackBtn;

    private PlayerManager playerManager;

    [Inject]
    private void Construct(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;
    }

    private void Awake()
    {
        interactBtn.onClick.AddListener(() =>
        {
            onInteract?.Invoke();
        });

        interactBtn.gameObject.SetActive(false);

        attackBtn.onClick.AddListener(() =>
        {
            onAttack?.Invoke();
        });

        attackBtn.gameObject.SetActive(false);
    }

    private void Update()
    {
        onMove?.Invoke(new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical));
    }

    private void OnDestroy()
    {
        if (!playerManager) return;

        playerManager.onPlayerSpawn -= OnPlayerSpawn;
    }

    private void OnPlayerSpawn(PlayerActorController playerActor)
    {

    }
}
