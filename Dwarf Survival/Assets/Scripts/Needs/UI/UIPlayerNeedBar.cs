using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIPlayerNeedBar : MonoBehaviour
{
    [SerializeField] private string needID;
    [SerializeField] private Slider needSlider;

    private PlayerManager playerManager;
    private NeedEntity needEntity;

    [Inject]
    public void Construct(PlayerManager playerManager)
    {
        this.playerManager = playerManager;
        this.playerManager.onPlayerSpawn += OnPlayerSpawn;
    }

    private void OnDestroy()
    {
        if (playerManager)
        {
            playerManager.onPlayerSpawn -= OnPlayerSpawn;
        }
    }

    private void OnPlayerSpawn(PlayerActorController playerActor)
    {
        if (needEntity != null)
        {
            needEntity.onValueChange -= OnNeedValueChange;
        }

        needEntity = playerActor.Actor.Needs[needID];

        needEntity.onValueChange += OnNeedValueChange;

        OnNeedValueChange(needEntity.Value);
        OnNeedMaxValueChange(needEntity.MaxValue);
    }

    private void OnNeedValueChange(float value)
    {
        needSlider.value = value;
    }

    private void OnNeedMaxValueChange(float maxValue)
    {
        needSlider.maxValue = maxValue;
    }
}
