using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    [SerializeField] private string poolID;

    public Action<PoolableObject> onDisable;

    public string PoolID { get => poolID; }

    private void OnDisable()
    {
        onDisable?.Invoke(this);
    }

    [Button]
    public void ChangePoolIDToObjectName()
    {
        poolID = gameObject.name.Replace(" ", "_").ToLower();
    }
}