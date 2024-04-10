using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIShortPlayerBuffs : MonoBehaviour
{
    [SerializeField] private UIShortBuffItem buffItemPrefab;
    [SerializeField] private Transform buffsContent;

    private PlayerManager playerManager;
    private ObjectPoolingManager poolingManager;
    private BuffsController playerBuffs;

    private Dictionary<string, UIShortBuffItem> spawnedItems;

    [Inject]
    private void Construct(PlayerManager playerManager, ObjectPoolingManager poolingManager)
    {
        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;

        this.poolingManager = poolingManager;

        spawnedItems = new Dictionary<string, UIShortBuffItem>();
    }

    private void OnDestroy()
    {
        if (!playerManager) return;

        playerManager.onPlayerSpawn -= OnPlayerSpawn;
    }

    private void OnPlayerSpawn(PlayerActorController player)
    {
        if (playerBuffs)
        {
            playerBuffs.onBuffAdd -= OnBuffAdd;
            playerBuffs.onBuffRemove -= OnBuffRemove;
        }

        playerBuffs = player.Actor.Buffs;
        playerBuffs.onBuffAdd += OnBuffAdd;
        playerBuffs.onBuffRemove += OnBuffRemove;
    }

    private void OnBuffAdd(BuffEntity buffEntity)
    {
        if (spawnedItems.ContainsKey(buffEntity.Data.ID)) return;

        UIShortBuffItem newBuffItem = poolingManager.GetPoolable(buffItemPrefab);
        newBuffItem.transform.SetParent(buffsContent);
        newBuffItem.transform.localScale = Vector3.one;
        newBuffItem.SetBuff(buffEntity);
        spawnedItems.Add(buffEntity.Data.ID, newBuffItem);
    }

    private void OnBuffRemove(BuffEntity buffEntity)
    {
        if (!spawnedItems.ContainsKey(buffEntity.Data.ID)) return;

        UIShortBuffItem newBuffItem = spawnedItems[buffEntity.Data.ID];
        newBuffItem.gameObject.SetActive(false);
        spawnedItems.Remove(buffEntity.Data.ID);
    }
}
