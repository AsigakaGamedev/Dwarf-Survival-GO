using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlayerWeaponSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image weaponImg;

    private WeaponsController weaponsController;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out UIMovableItem droppedItem))
        {
            ItemEntity entity = droppedItem.LinkedItem;
            ItemInfo info = entity.Info;

            if (!info.IsWeapon) return;

            if (weaponsController.TryEquip(entity))
            {
                weaponImg.sprite = info.EquipmentIcon;
                weaponImg.gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        if (!weaponsController && PlayerManager.Instance)
        {
            weaponsController = PlayerManager.Instance.PlayerInstance.Actor.Weapons;
            weaponImg.gameObject.SetActive(false);
        }
    }
}
