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

    [Space]
    [ReadOnly, SerializeField] private ItemEntity equipedEntity;
    [ReadOnly, SerializeField] private EquipmentModel equipedModel;

    public int Id { get => id; }

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

        if (equipedModel) equipedModel.OnDeqiup();

        equipedModel = models[equipedEntity.Info];
        equipedModel.OnEquip();
    }
}
