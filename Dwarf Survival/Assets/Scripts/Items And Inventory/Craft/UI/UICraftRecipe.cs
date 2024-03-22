using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftRecipe : PoolableObject
{
    [SerializeField] private Image iconImg;

    [Space]
    [ReadOnly, SerializeField] private CraftInfo info;

    public CraftInfo Info { get => info; }

    public void SetInfo(CraftInfo info)
    {
        this.info = info;

        iconImg.sprite = info.RecipeIcon;
    }
}
