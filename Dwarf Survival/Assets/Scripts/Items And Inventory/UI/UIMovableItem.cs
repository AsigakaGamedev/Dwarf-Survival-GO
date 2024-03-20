using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMovableItem : UIMovableObject
{
    private ItemEntity linkedItem;

    public void SetItem(ItemEntity item)
    {
        linkedItem = item;

        iconImg.sprite = linkedItem.Info.CellIcon;
    }

    protected override void OnBegin(PointerEventData eventData)
    {
        iconImg.sprite = linkedItem.Info.MoveIcon;
    }

    protected override void OnEnd(PointerEventData eventData)
    {
        iconImg.sprite = linkedItem.Info.CellIcon;
    }
}
