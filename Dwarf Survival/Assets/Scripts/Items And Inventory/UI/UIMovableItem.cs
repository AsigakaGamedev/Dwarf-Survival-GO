using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class UIMovableItem : UIMovableObject
{
    [SerializeField] private UIInventoryCell linkedCell;

    private UIPopupsManager popupsManager;
    private ItemEntity linkedItem;

    public UIInventoryCell LinkedCell { get => linkedCell; }
    public ItemEntity LinkedItem { get => linkedItem; }

    [Inject]
    private void Construct(UIPopupsManager popupsManager)
    {
        this.popupsManager = popupsManager;
    }

    public void SetItem(ItemEntity item)
    {
        linkedItem = item;

        if (linkedItem == null || linkedItem.Info == null) return;

        iconImg.sprite = linkedItem.Info.CellIcon;
    }

    protected override void OnBegin(PointerEventData eventData)
    {
        if (linkedItem == null || linkedItem.Info == null) return;

        iconImg.sprite = linkedItem.Info.MoveIcon;
    }

    protected override void OnEnd(PointerEventData eventData)
    {
        if (linkedItem == null || linkedItem.Info == null) return;
        
        iconImg.sprite = linkedItem.Info.CellIcon;
    }

    protected override void OnClick(PointerEventData eventData)
    {
        if (linkedItem == null) return;

        UISelectedItemPopup selectedItemPopup = popupsManager.OpenPopup<UISelectedItemPopup>("select_item");
        selectedItemPopup.SelectItem(linkedItem);
    }
}
