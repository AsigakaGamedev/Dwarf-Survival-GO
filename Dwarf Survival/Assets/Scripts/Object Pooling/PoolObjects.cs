using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolObjects<T> where T : PoolableObject
{
    private T prefab;
    private bool expanding;
    private Transform parent;
    private List<T> objectsInPool;
    private DiContainer diContainer;

    public PoolObjects(T prefab, int startCount, bool expanding, DiContainer diContainer, Transform parent = null)
    {
        this.prefab = prefab;
        this.expanding = expanding;
        this.parent = parent;
        this.diContainer = diContainer;
        objectsInPool = new List<T>();

        for (int i = 0; i < startCount; i++)
        {
            CreateNewObject();
        }
    }

    public T CreateNewObject()
    {
        T newObj = diContainer.InstantiatePrefabForComponent<T>(prefab, parent);
        newObj.gameObject.SetActive(false);
        objectsInPool.Add(newObj);
        return newObj;
    }

    public T CreateNewObject(Vector3 spawnPos)
    {
        T newObj = Object.Instantiate(prefab, spawnPos, Quaternion.identity, parent);
        newObj.gameObject.SetActive(false);
        objectsInPool.Add(newObj);
        return newObj;
    }

    public T GetObject()
    {
        foreach (T obj in objectsInPool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        if (expanding)
        {
            T newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        return null;
    }

    public T GetObject(Vector3 spawnPos)
    {
        foreach (T obj in objectsInPool)
        {
            if (!obj.gameObject.activeSelf)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        if (expanding)
        {
            T newObj = CreateNewObject(spawnPos);
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        return null;
    }
}
