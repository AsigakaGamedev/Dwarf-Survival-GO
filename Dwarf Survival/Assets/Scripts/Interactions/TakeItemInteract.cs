using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemInteract : AInteractionComponent
{
    [SerializeField] private ItemData itemData;

    public override void OnInteract(Actor actor)
    {
        if (actor.Inventory)
        {
            actor.Inventory.AddItem(itemData);
            Asigaka.UI.UIEffectsManager.Instance.ShowMovableEffect(transform.position, "player_inventory", itemData.Info.MoveIcon);
        }
    }
}
