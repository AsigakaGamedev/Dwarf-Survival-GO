using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableObject : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private float maxMineCapacity;

    [Space]
    [SerializeField] private ObjectSpawnData[] miningResults;

    [Header("Effects")]
    [SerializeField] private float mineShakeStrength = 1;
    [SerializeField] private float mineShakeDuration = 1;

    [Space]
    [ReadOnly, SerializeField] private float mineCapacity;

    private ObjectPoolingManager poolingManager;

    private bool canShake;

    private void Start()
    {
        poolingManager = ServiceLocator.GetService<ObjectPoolingManager>();

        canShake = true;
        mineCapacity = maxMineCapacity;
    }

    public void Mine(float power)
    {
        if (canShake)
        {
            transform.DOShakeScale(mineShakeDuration, mineShakeStrength);
            canShake = false;
            Invoke(nameof(ResetCanShake), mineShakeDuration);
        }

        mineCapacity -= power;

        if (mineCapacity <= 0)
        {
            foreach (ObjectSpawnData result in miningResults)
            {
                if (!result.CanSpawn()) continue;

                for (int i = 0; i < result.SpawnAmount; i++)
                {
                    PoolableObject newResult = poolingManager.GetPoolable(result.Prefab);
                    newResult.transform.position = transform.position;
                }
            }

            gameObject.SetActive(false);
        }
    }

    private void ResetCanShake()
    {
        canShake = true;
    }
}
