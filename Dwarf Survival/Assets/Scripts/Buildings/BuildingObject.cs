using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingState { Planning, Deactivated, Activated}

public class BuildingObject : PoolableObject
{
    [Space]
    [SerializeField] private string buildingName;
    [SerializeField] private string buildingDesc;

    [Space]
    [Space]
    [SerializeField] private Sprite icon;
    [SerializeField] private BuildingType type;

    [Space]
    [SerializeField] private ItemData[] neededItems;

    [Space]
    [SerializeField] private Collider2D mainCollider;

    [Header("States")]
    [SerializeField] private GameObject planningObj;
    [SerializeField] private GameObject deactivatedObj;
    [SerializeField] private GameObject activatedObj;

    [Space]
    [ReadOnly, SerializeField] private BuildingState state;

    public Sprite Icon { get => icon; }
    public BuildingType Type { get => type; }

    public string BuildingName { get => buildingName; }
    public string BuildingDesc { get => buildingDesc; }

    public ItemData[] NeededItems { get => neededItems; }

    public void SetState(BuildingState newState)
    {
        state = newState;

        planningObj.SetActive(state == BuildingState.Planning);
        deactivatedObj.SetActive(state == BuildingState.Deactivated);
        activatedObj.SetActive(state == BuildingState.Activated);

        mainCollider.isTrigger = state == BuildingState.Planning;
    }
}
