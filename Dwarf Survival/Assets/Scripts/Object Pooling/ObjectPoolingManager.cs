using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPoolingManager : MonoBehaviour
{
    private DiContainer diContainer;
    private Dictionary<string, PoolObjects<PoolableObject>> cachedPoolables;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }

    private void OnEnable()
    {
        if (cachedPoolables != null) return;

        cachedPoolables = new Dictionary<string, PoolObjects<PoolableObject>>();
    }

    public T GetPoolable<T>(T prefab, int poolableCount = 2) where T : PoolableObject
    {
        if (cachedPoolables.ContainsKey(prefab.PoolID))
        {
            return cachedPoolables[prefab.PoolID].GetObject() as T;
        }
        else
        {
            PoolObjects<PoolableObject> newPool = new PoolObjects<PoolableObject>(prefab, poolableCount, true, diContainer);
            cachedPoolables.Add(prefab.PoolID, newPool);
            return cachedPoolables[prefab.PoolID].GetObject() as T;
        }
    }

    public T GetPoolable<T>(T prefab, Vector3 spawnPos, int poolableCount = 2) where T : PoolableObject
    {
        if (cachedPoolables.ContainsKey(prefab.PoolID))
        {
            return cachedPoolables[prefab.PoolID].GetObject() as T;
        }
        else
        {
            PoolObjects<PoolableObject> newPool = new PoolObjects<PoolableObject>(prefab, poolableCount, true, diContainer);
            cachedPoolables.Add(prefab.PoolID, newPool);
            return cachedPoolables[prefab.PoolID].GetObject() as T;
        }
    }
}