using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacteristicEntity 
{
    [SerializeField] private float curValue;
    [SerializeField] private float maxValue;

    public CharacteristicEntity(float curValue, float maxValue)
    {
        this.curValue = curValue;
        this.maxValue = maxValue;
    }

    public float CurValue { get => curValue;
        set
        {
            curValue = Mathf.Clamp(value, 0, maxValue);
        }
    }
    public float MaxValue { get => maxValue; set => maxValue = value; }
}
