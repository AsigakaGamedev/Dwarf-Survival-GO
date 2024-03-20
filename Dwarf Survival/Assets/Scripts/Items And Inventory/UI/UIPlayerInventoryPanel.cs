using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInventoryPanel : UIInventoryPanel
{
    private AInventory playerInventory;

    private void OnEnable()
    {
        if (!playerInventory && PlayerManager.Instance) playerInventory = PlayerManager.Instance.PlayerInstance.Actor.Inventory;

        if (playerInventory)
        {
            ShowInventory(playerInventory);
        }
    }
}
