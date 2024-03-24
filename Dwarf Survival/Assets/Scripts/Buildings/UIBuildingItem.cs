using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBuildingItem : PoolableObject, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Space]
    [SerializeField] private Image iconImg;
    [SerializeField] private CanvasGroup group;

    private BuildingObject linkedPrefab;

    public Action<UIBuildingItem> onBeginDrag;
    public Action<UIBuildingItem> onDrag;
    public Action<UIBuildingItem> onEndDrag;
    public Action<UIBuildingItem> onClick;

    public BuildingObject LinkedPrefab { get => linkedPrefab; }
    public Image IconImg { get => iconImg; }

    public void SetPrefab(BuildingObject prefab)
    {
        linkedPrefab = prefab;
        iconImg.sprite = prefab.Icon;
    }

    public void SetIconAlpha(float alpha)
    {
        group.alpha = alpha;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(this);
    }
}
