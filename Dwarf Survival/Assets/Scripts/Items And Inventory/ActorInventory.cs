using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorInventory : AInventory, IInitListener
{
    public void OnInitialize()
    {
        cells = new List<InventoryCellEntity>();
    }
}
