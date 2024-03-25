using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TakeItemInteract : AInteractionComponent
{
    [SerializeField] private ItemData itemData;

    private Asigaka.UI.UIEffectsManager uiEffectsManager;

    [Inject]
    public void Construct(Asigaka.UI.UIEffectsManager uiEffectsManager)
    {
        this.uiEffectsManager = uiEffectsManager;
    }

    public override void OnInteract(Actor actor)
    {
        if (actor.Inventory)
        {
            actor.Inventory.AddItem(itemData);
            uiEffectsManager.ShowMovableEffect(transform.position, "player_inventory", itemData.Info.MoveIcon);
        }
    }
}
