using AYellowpaper.SerializedCollections;
using ModestTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, BuffEntity> liveBuffs;

    public Action<BuffEntity> onBuffAdd;
    public Action<BuffEntity> onBuffRemove;

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
                    onBuffRemove?.Invoke(value);
                    liveBuffs.Remove(id);
                }
            }
        }
        catch { }
    }

    public void AddBuff(BuffData newBuff)
    {
        if (!liveBuffs.ContainsKey(newBuff.ID))
        {
            BuffEntity newBuffEntity = new BuffEntity(newBuff);
            liveBuffs.Add(newBuff.ID, newBuffEntity);
            onBuffAdd?.Invoke(newBuffEntity);
        }
    }
}

[System.Serializable] 
public class BuffEntity
{
    private float lifeTime;

    public BuffData Data;

    public Action<float> onLifeTimeChange;

    public float LifeTime { get => lifeTime; 
        set
        {
            lifeTime = value;
            onLifeTimeChange?.Invoke(lifeTime);
        }
    }

    public BuffEntity(BuffData data)
    {
        Data = data;
        lifeTime = data.LifeTime;
    }
}
