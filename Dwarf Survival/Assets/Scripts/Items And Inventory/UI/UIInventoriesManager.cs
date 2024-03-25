using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIInventoriesManager : MonoBehaviour
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

    private void Awake()
    {
        foreach (var panel in inventoryPanels.Values)
        {
            panel.Init();
        }
    }
}
