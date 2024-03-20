using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryCell : PoolableObject, IDropHandler
{
    [Space]
    [SerializeField] private UIMovableItem linkedItem;

    private InventoryCellEntity entity;

    public InventoryCellEntity Entity { get => entity; }

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
            linkedItem.SetItem(null);
            linkedItem.gameObject.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UIMovableItem droppedItem))
        {
            ItemEntity transferItem = droppedItem.LinkedItem;
            droppedItem.LinkedCell.Entity.ItemInCell = linkedItem.LinkedItem;
            entity.ItemInCell = transferItem;

            droppedItem.LinkedCell.UpdateCell();
            UpdateCell();
            droppedItem.OnEndDrag(null);
            linkedItem.OnEndDrag(null);
        }
    }
}
