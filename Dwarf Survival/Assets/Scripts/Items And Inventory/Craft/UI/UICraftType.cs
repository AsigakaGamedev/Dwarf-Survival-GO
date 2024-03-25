using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICraftType : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private CraftType type;

    public Action<CraftType> onTypeChange;

    public void Init()
    {
        btn.onClick.AddListener(() =>
        {
            onTypeChange?.Invoke(type);
        });
    }
}
