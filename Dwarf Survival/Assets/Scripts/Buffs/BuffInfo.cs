using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Buff")]
public class BuffInfo : ScriptableObject
{
    [SerializeField] private BuffData buffData;

    public BuffData BuffData { get => buffData; }
}
