using AYellowpaper.SerializedCollections;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PoolableObject))]
public class BaseObject : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private BaseLevel[] allLevels;

    [Space]
    [ReadOnly, SerializeField] private BaseLevel currentLevel;
    [ReadOnly, SerializeField] private int currentLevelIndex;

    [Header("Evolution")]
    [SerializeField] private float evolutionPointsPerTime = 5;

    [Space]
    [ReadOnly, SerializeField] private float earnedEvolutionPoints;

    [Header("Production")]
    [SerializeField] private float productionPointsPerTime = 1;

    [Space]
    [ReadOnly, SerializeField] private float earnedProductionPoints;
    [ReadOnly, SerializeField] private SerializedDictionary<string, BaseSpawnableObject> spawnableObjectsDict;

    private ObjectPoolingManager poolingManager;

    [Inject]
    private void Construct(ObjectPoolingManager poolingManager)
    {
        this.poolingManager = poolingManager;
    }

    private void Start()
    {
        foreach (var level in allLevels)
        {
            level.gameObject.SetActive(false);
        }

        currentLevel = allLevels[0];
        currentLevelIndex = 0;
        currentLevel.OnEarnLevel();

        earnedProductionPoints = 0;

        spawnableObjectsDict = new SerializedDictionary<string, BaseSpawnableObject>();

        foreach (BaseSpawnableObject levelObject in currentLevel.BaseObjects)
        {
            spawnableObjectsDict.Add(levelObject.Prefab.PoolID, levelObject);
        }

        StartCoroutine(EProductObjects());
    }

    private void Update()
    {
        ProductionLoop();
        EvolutionLoop();
    }

    private void ProductionLoop()
    {
        earnedProductionPoints += Time.deltaTime * productionPointsPerTime;
    }

    private IEnumerator EProductObjects()
    {
        while (true)
        {
            foreach (BaseSpawnableObject levelObject in currentLevel.BaseObjects)
            {
                if (levelObject.MaxCount > levelObject.SpawnedCount && 
                    levelObject.ProductionCost <= earnedProductionPoints)
                {
                    PoolableObject newObject = poolingManager.GetPoolable(levelObject.Prefab);
                    newObject.transform.position = transform.position;

                    levelObject.SpawnedCount++;
                    earnedProductionPoints -= levelObject.ProductionCost;
                }
            }

            yield return new WaitForSeconds(2);
        }
    }

    private void EvolutionLoop()
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

            foreach (BaseSpawnableObject levelObject in currentLevel.BaseObjects)
            {
                if (!spawnableObjectsDict.TryAdd(levelObject.Prefab.PoolID, levelObject))
                {
                    spawnableObjectsDict[levelObject.Prefab.PoolID] = levelObject;
                }
            }
        }
    }
}
