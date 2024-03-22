using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftRecipe : PoolableObject, IInitListener, IDeinitListener
{
    [SerializeField] private Button btn;
    [SerializeField] private Image iconImg;

    [Space]
    [ReadOnly, SerializeField] private CraftInfo info;

    public Action<CraftInfo> onSelectClick;

    public CraftInfo Info { get => info; }

    public void OnInitialize()
    {
        btn.onClick.AddListener(() =>
        {
            onSelectClick?.Invoke(info);
        });
    }

    public void OnDeinitialize()
    {
        btn.onClick.RemoveAllListeners();
    }

    public void SetInfo(CraftInfo info)
    {
        this.info = info;

        iconImg.sprite = info.RecipeIcon;
    }
}
