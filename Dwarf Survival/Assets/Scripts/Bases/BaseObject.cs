using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolableObject))]
public class BaseObject : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private BaseLevel[] allLevels;
    [SerializeField] private float evolutionPointsPerTime = 5;

    [Space]
    [ReadOnly, SerializeField] private BaseLevel currentLevel;
    [ReadOnly, SerializeField] private int currentLevelIndex;
    [ReadOnly, SerializeField] private float earnedEvolutionPoints;

    private void Start()
    {
        foreach (var level in allLevels)
        {
            level.gameObject.SetActive(false);
        }

        currentLevel = allLevels[0];
        currentLevelIndex = 0;
        currentLevel.OnEarnLevel();
    }

    private void Update()
    {
        if (currentLevelIndex + 1 >= allLevels.Length) return;

        earnedEvolutionPoints += Time.deltaTime * evolutionPointsPerTime;

        if (earnedEvolutionPoints > allLevels[currentLevelIndex + 1].TargetEvolutionPoints)
        {
            earnedEvolutionPoints = 0;
            currentLevel.gameObject.SetActive(false);

            currentLevelIndex++;

            currentLevel = allLevels[currentLevelIndex];
            currentLevel.OnEarnLevel();
        }
    }
}
