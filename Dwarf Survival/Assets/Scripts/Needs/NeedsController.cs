using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsController : MonoBehaviour
{
    [SerializeField] private SerializedDictionary<string, NeedEntity> allNeeds;

    public void UpdateNeeds()
    {
        foreach (NeedEntity need in allNeeds.Values)
        {
            need.Value += need.TimeValue * Time.deltaTime;
        }
    }

    public bool TryGetNeed(string key, out NeedEntity need)
    {
        if (allNeeds.ContainsKey(key))
        {
            need = allNeeds[key];
            return true;
        }

        need = null;
        return false;
    }

    public NeedEntity this[string id]
    {
        get { return allNeeds[id]; }
    }
}
