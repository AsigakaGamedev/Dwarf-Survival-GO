using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftRecipe : PoolableObject
{
    [SerializeField] private Button btn;
    [SerializeField] private Image iconImg;

    [Space]
    [ReadOnly, SerializeField] private CraftInfo info;

    public Action<CraftInfo> onSelectClick;

    public CraftInfo Info { get => info; }

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            onSelectClick?.Invoke(info);
        });
    }

    public void SetInfo(CraftInfo info)
    {
        this.info = info;

        iconImg.sprite = info.RecipeIcon;
    }
}
