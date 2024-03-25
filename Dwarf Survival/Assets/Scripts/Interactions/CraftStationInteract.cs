using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CraftStationInteract : AInteractionComponent
{
    [SerializeField] private string stationName;
    [SerializeField] private CraftInfo[] stationCrafts;

    private UIManager uiManager;
    private UIPlayerCraftsManager uiPlayerCrafts;

    [Inject]
    public void Construct(UIManager uiManager, UIPlayerCraftsManager uiPlayerCrafts)
    {
        this.uiManager = uiManager;
        this.uiPlayerCrafts = uiPlayerCrafts;
    }

    public override void OnInteract(Actor actor)
    {
        uiPlayerCrafts.Open(stationCrafts, actor.Inventory, stationName);
        uiManager.ChangeScreen("craft");
    }
}
