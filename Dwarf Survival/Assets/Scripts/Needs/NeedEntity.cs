using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeedEntity 
{
    [SerializeField] private float value;
    [SerializeField] private float maxValue;
    [SerializeField] private float timeValue;

    public Action<float> onValueChange;

    public float Value { get => value;
        set
        {
            this.value = Mathf.Clamp(value, 0, maxValue);
            onValueChange?.Invoke(this.value);
        }
    }
    public float MaxValue { get => maxValue; set => maxValue = value; }
    public float TimeValue { get => timeValue; set => timeValue = value; }
}
