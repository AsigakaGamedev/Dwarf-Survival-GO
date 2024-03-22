using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftStationInteract : AInteractionComponent
{
    [SerializeField] private string stationName;
    [SerializeField] private CraftInfo[] stationCrafts;

    public override void OnInteract(Actor actor)
    {
        UIPlayerCraftsManager.Instance.Open(stationCrafts, actor.Inventory, stationName);
        ServiceLocator.GetService<UIManager>().ChangeScreen("craft");
    }
}
