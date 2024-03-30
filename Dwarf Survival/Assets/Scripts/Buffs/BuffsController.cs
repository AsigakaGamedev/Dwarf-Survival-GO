using AYellowpaper.SerializedCollections;
using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, BuffEntity> liveBuffs;

    public Action<BuffData> onBuffAdd;
    public Action<BuffData> onBuffRemove;

    public void Init()
    {
        liveBuffs = new SerializedDictionary<string, BuffEntity>();
    }

    public void UpdateBuffs()
    {
        //Да костыль, а что вы мне сделаете?
        try
        {
            foreach ((string id, BuffEntity value) in liveBuffs)
            {
                value.LifeTime -= Time.deltaTime;

                if (value.LifeTime <= 0)
                {
                    onBuffRemove?.Invoke(value.Data);
                    liveBuffs.Remove(id);
                }
            }
        }
        catch { }
    }

    public void AddBuff(BuffData newBuff)
    {
        if (liveBuffs.TryAdd(newBuff.Id, new BuffEntity(newBuff)))
        {
            onBuffAdd?.Invoke(newBuff);
        }
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
