using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacteristicEntity 
{
    [SerializeField] private float curValue;

    public CharacteristicEntity(float curValue)
    {
        this.curValue = curValue;
    }

    public float Value { get => curValue;
        set
        {
            curValue = value;
        }
    }
}
