using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevel : MonoBehaviour
{
    [SerializeField] private float targetEvolutionPoints;

    public float TargetEvolutionPoints { get => targetEvolutionPoints; }

    public void OnEarnLevel()
    {
        gameObject.SetActive(true);
    }
}
