using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICraftNeededItem : PoolableObject
{
    [SerializeField] private Image itemImg;
    [SerializeField] private TextMeshProUGUI itemAmountTxt;

    public void SetData(Sprite icon, int curAmount, int neededAmount)
    {
        itemImg.sprite = icon;

        itemAmountTxt.text = neededAmount <= curAmount
            ? $"<color=\"green\">{curAmount}/{neededAmount}</color>"
            : $"<color=\"red\">{curAmount}/{neededAmount}</color>";
    }
}
