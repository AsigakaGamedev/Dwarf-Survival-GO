using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BuffData 
{
    [SerializeField] private string id;

    [Space]
    [ShowAssetPreview, SerializeField] private Sprite icon; 

    [Space]
    [SerializeField] private float lifeTime;
    [SerializeField] private SerializedDictionary<string, float> characteristics;

    public string ID { get => id; }

    public Sprite Icon { get => icon; }

    public float LifeTime { get => lifeTime; }
    public SerializedDictionary<string, float> Characteristics { get => characteristics; }
}
