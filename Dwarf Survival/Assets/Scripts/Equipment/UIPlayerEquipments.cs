using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEquipments : MonoBehaviour
{
    [SerializeField] private UIEquipmentSlot[] slots;

    private EquipmentsController playerEquipments;

    private void OnEnable()
    {
        if (!playerEquipments && PlayerManager.Instance)
        {
            playerEquipments = PlayerManager.Instance.PlayerInstance.Actor.Equipments;

            foreach (var slot in slots)
            {
                slot.SetController(playerEquipments);
            }
        }
    }
}
