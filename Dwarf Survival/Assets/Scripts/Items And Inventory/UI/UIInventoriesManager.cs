using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventoriesManager : MonoBehaviour, IInitListener
{
    [SerializeField] private SerializedDictionary<string, UIInventoryPanel> inventoryPanels;

    [Button]
    public void FindAllPanels()
    {
        List<UIInventoryPanel> allPanels = GetComponentsInChildren<UIInventoryPanel>().ToList();

        foreach (UIInventoryPanel panel in allPanels)
        {
            if (!inventoryPanels.ContainsValue(panel))
            {
                inventoryPanels.Add("", panel);
            }
        }
    }

    public void OnInitialize()
    {
        foreach (var panel in inventoryPanels.Values)
        {
            panel.OnInitialize();
        }
    }
}
