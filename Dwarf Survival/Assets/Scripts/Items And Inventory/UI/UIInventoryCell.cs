using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryCell : PoolableObject
{
    [Space]
    [SerializeField] private UIMovableItem linkedItem;

    private InventoryCellEntity entity;

    public void SetEntity(InventoryCellEntity entity)
    {
        this.entity = entity;

        UpdateCell();
    }

    public void SetItemParen(Transform dragParent)
    {
        linkedItem.SetParents(transform, dragParent);
    }

    public void UpdateCell()
    {
        if (entity.ItemInCell != null && entity.ItemInCell.Info)
        {
            linkedItem.SetItem(entity.ItemInCell);
            linkedItem.gameObject.SetActive(true);
        }
        else
        {
            linkedItem.gameObject.SetActive(false);
        }
    }
}
