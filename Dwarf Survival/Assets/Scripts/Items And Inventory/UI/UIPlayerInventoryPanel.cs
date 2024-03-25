using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Zenject;

public class UIPlayerInventoryPanel : UIInventoryPanel
{
    private PlayerManager playerManager;
    private AInventory playerInventory;

    private void OnEnable()
    {
        if (playerInventory)
        {
            ShowInventory(playerInventory);
        }
    }

    [Inject]
    public void Construct(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;
    }

    private void OnDestroy()
    {
        if (playerManager)
        {
            playerManager.onPlayerSpawn -= OnPlayerSpawn;
        }
    }

    private void OnPlayerSpawn(PlayerActorController playerActor)
    {
        playerInventory = playerActor.Actor.Inventory;
    }
}
