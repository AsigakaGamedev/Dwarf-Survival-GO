using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Info")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private Sprite icon;

    public Sprite Icon { get => icon; }
}
