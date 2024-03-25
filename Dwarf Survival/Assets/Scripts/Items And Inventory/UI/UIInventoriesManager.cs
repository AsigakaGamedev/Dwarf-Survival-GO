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
        UIInventoryPanel[] allPanels = GetComponentsInChildren<UIInventoryPanel>(true);

        foreach (UIInventoryPanel panel in allPanels)
        {
            if (panel is not UIPlayerInventoryPanel && !inventoryPanels.ContainsValue(panel))
            {
                inventoryPanels.Add($"panel {inventoryPanels.Count}", panel);
            }
        }
    }

    private void OnValidate()
    {
        FindAllPanels();
    }

    private void Awake()
    {
        foreach (var panel in inventoryPanels.Values)
        {
            panel.Init();
        }
    }

    public UIInventoryPanel GetPanel(string key)
    {
        return inventoryPanels[key];
    }
}
