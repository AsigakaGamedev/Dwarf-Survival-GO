using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private int id;

    [Space]
    [SerializeField] private SerializedDictionary<ItemInfo, EquipmentModel> models;

    private ItemEntity equipedEntity;
    private EquipmentModel equipedModel;

    public int Id { get => id; }
    public ItemEntity EquipedEntity { get => equipedEntity; }

    [Button]
    public void PreInitialize()
    {
        equipedEntity = null;
        equipedModel = null;

        foreach (var model in models.Values)
        {
            model.gameObject.SetActive(false);
        }
    }

    public void Equip(ItemEntity equipableItem)
    {
        equipedEntity = equipableItem;

        DequipCurrent();

        equipedModel = models[equipedEntity.Info];
        equipedModel.OnEquip();
    }

    public void DequipCurrent()
    {
        if (equipedModel)
        {
            equipedModel.OnDeqiup();
            equipedModel = null;
            equipedEntity = null;
        }
    }
}
