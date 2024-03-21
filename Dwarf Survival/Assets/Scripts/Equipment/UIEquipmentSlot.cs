using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEquipmentSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int slotID;

    [Space]
    [SerializeField] private Image equipmentImg;

    private EquipmentsController equipmentController;

    public void SetController(EquipmentsController equipmentController)
    {
        this.equipmentController = equipmentController;
        equipmentImg.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UIMovableItem droppedItem))
        {
            ItemEntity entity = droppedItem.LinkedItem;
            ItemInfo info = entity.Info;

            if (!info.IsEquipable || info.EquipSlotID != slotID) return;

            if (equipmentController.TryEquip(entity))
            {
                equipmentImg.sprite = info.EquipmentIcon;
                equipmentImg.gameObject.SetActive(true);
            }
        }
    }
}
