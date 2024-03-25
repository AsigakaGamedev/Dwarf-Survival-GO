using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UIPlayerWeaponSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image weaponImg;

    private PlayerManager playerManager;
    private WeaponsController weaponsController;

    [Inject]
    public void Construct(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;
    }

    private void OnDestroy()
    {
        if (playerManager)
        {
            playerManager.onPlayerSpawn -= OnPlayerSpawn;
        }
    }

    private void OnPlayerSpawn(PlayerActorController playerActor)
    {
        weaponImg.gameObject.SetActive(false);
        weaponsController = playerActor.Actor.Weapons;
    }

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
}
