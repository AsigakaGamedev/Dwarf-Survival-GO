using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryCell : PoolableObject, IDropHandler
{
    [Space]
    [SerializeField] private UIMovableItem linkedItem;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    private InventoryCellEntity entity;

    public InventoryCellEntity Entity { get => entity; }

    public void SetEntity(InventoryCellEntity entity)
    {
        if (this.entity != null)
        {
            this.entity.onItemChange -= UpdateCell;
            this.entity.onItemAmountChange -= UpdateAmount;
        }

        this.entity = entity;
        this.entity.onItemChange += UpdateCell;
        this.entity.onItemAmountChange += UpdateAmount;
        UpdateCell(this.entity.ItemInCell);
    }

    public void SetItemParent(Transform dragParent)
    {
        linkedItem.SetParents(transform, dragParent);
    }

    public void UpdateCell(ItemEntity itemEntity)
    {
        if (itemEntity != null && itemEntity.Info)
        {
            linkedItem.SetItem(itemEntity);
            linkedItem.gameObject.SetActive(true);
            itemAmountText.text = itemEntity.Amount.ToString();
            itemAmountText.gameObject.SetActive(true);
        }
        else
        {
            linkedItem.SetItem(null);
            linkedItem.gameObject.SetActive(false);
            itemAmountText.gameObject.SetActive(false);
        }
    }

    private void UpdateAmount(int amount)
    {
        itemAmountText.text = amount.ToString();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UIMovableItem droppedItem))
        {
            droppedItem.OnEndDrag(null);
            linkedItem.OnEndDrag(null);

            InventoryCellEntity droppedCell = droppedItem.LinkedCell.Entity;

            ItemEntity transferItem = droppedItem.LinkedItem;

            if (!droppedCell.CanAddItem(linkedItem.LinkedItem) ||
                !entity.CanAddItem(transferItem)) return;

            if (droppedCell.ItemInCell != null && 
                entity.ItemInCell != null &&
                droppedCell.ItemInCell.Info == entity.ItemInCell.Info)
            {
                droppedCell.TryAddItem(null);
                entity.TryAddItem(transferItem);
                return;
            }

            droppedCell.TryAddItem(linkedItem.LinkedItem);
            entity.TryAddItem(transferItem);
        }
    }
}
