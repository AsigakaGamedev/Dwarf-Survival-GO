using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="World/Preset")]
public class WorldPreset : ScriptableObject
{
    [SerializeField] private string presetName;

    [Space]
    [SerializeField] private int size = 64;
    [SerializeField] private float scale = 5;

    [Space]
    [SerializeField] private WorldBiomeInfoData[] biomes;

    [Space]
    [SerializeField] private WorldObjectData[] objects;

    public string PresetName { get => presetName; }

    public int Size { get => size; }
    public float Scale { get => scale; }

    public WorldBiomeInfoData[] Biomes { get => biomes; }

    public WorldObjectData[] Objects { get => objects; }
}
