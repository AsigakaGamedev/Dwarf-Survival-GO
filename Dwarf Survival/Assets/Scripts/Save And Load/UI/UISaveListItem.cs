using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISaveListItem : PoolableObject
{
    [SerializeField] private TextMeshProUGUI saveNameText;
    [SerializeField] private Button btn;

    private SaveEntity entity;

    public Action<SaveEntity> onSaveSelect;

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            onSaveSelect?.Invoke(entity);
        });
    }

    public void SetEntity(SaveEntity entity)
    {
        this.entity = entity;

        saveNameText.text = entity.Name;
    }
}
