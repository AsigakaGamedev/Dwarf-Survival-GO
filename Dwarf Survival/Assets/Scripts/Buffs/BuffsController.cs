using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsController : MonoBehaviour
{
    [SerializeField] private List<BuffEntity> liveBuffs;

    public Action<BuffData> onBuffAdd;
    public Action<BuffData> onBuffRemove;

    public void Init()
    {
        liveBuffs = new List<BuffEntity>();
    }

    public void UpdateBuffs()
    {
        for (int i = 0; i < liveBuffs.Count; i++)
        {
            liveBuffs[i].LifeTime -= Time.deltaTime;

            if (liveBuffs[i].LifeTime <= 0)
            {
                liveBuffs.RemoveAt(i);
            }
        }
    }

    public void AddBuff(BuffData newBuff)
    {
        liveBuffs.Add(new BuffEntity(newBuff));
        onBuffAdd?.Invoke(newBuff);
    }
}

[System.Serializable] 
public class BuffEntity
{
    public BuffData Data;
    public float LifeTime;

    public BuffEntity(BuffData data)
    {
        Data = data;
        LifeTime = data.LifeTime;
    }
}
