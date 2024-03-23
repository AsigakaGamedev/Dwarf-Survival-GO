using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerNeedBar : MonoBehaviour, IInitListener, IDeinitListener
{
    [SerializeField] private string needID;
    [SerializeField] private Slider needSlider;

    private NeedEntity needEntity;

    public void OnInitialize()
    {
        needEntity = PlayerManager.Instance.PlayerInstance.Actor.Needs[needID];

        needEntity.onValueChange += OnNeedValueChange;

        OnNeedValueChange(needEntity.Value);
        OnNeedMaxValueChange(needEntity.MaxValue);
    }

    public void OnDeinitialize()
    {
        needEntity.onValueChange -= OnNeedValueChange;
    }

    private void OnNeedValueChange(float value)
    {
        needSlider.value = value;
    }

    private void OnNeedMaxValueChange(float maxValue)
    {
        needSlider.maxValue = maxValue;
    }
}
