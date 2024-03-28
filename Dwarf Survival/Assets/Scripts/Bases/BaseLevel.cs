using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    [SerializeField] private float targetEvolutionPoints;

    [Space]
    [SerializeField] private BaseSpawnableObject[] baseObjects;

    public float TargetEvolutionPoints { get => targetEvolutionPoints; }

    public BaseSpawnableObject[] BaseObjects { get => baseObjects; }

    public void OnEarnLevel()
    {
        gameObject.SetActive(true);
    }
}
