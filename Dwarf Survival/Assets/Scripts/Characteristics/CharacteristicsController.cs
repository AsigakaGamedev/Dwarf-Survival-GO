using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, CharacteristicEntity> characteristics;

    public void AddCharacteristic(string id, CharacteristicEntity entity)
    {
        characteristics.TryAdd(id, entity);
    }

    public CharacteristicEntity this[string id]
    {
        get { return characteristics[id]; }
    }
}
