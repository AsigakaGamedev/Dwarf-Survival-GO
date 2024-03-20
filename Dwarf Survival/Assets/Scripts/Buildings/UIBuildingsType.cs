using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingsType : MonoBehaviour
{
    [SerializeField] private BuildingType buildingType;
    [SerializeField] private Button btn;

    public Action<BuildingType> onTypeChange;

    private void Awake()
    {
        btn.onClick.AddListener(() =>
        {
            onTypeChange?.Invoke(buildingType);
        });
    }
}
