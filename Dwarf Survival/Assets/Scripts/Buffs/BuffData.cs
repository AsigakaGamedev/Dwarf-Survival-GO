using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BuffData 
{
    [SerializeField] private float lifeTime;
    [SerializeField] private SerializedDictionary<string, float> characteristics;

    public float LifeTime { get => lifeTime; }
    public SerializedDictionary<string, float> Characteristics { get => characteristics; }
}
