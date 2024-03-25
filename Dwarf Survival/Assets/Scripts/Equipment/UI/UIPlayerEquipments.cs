using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIPlayerEquipments : MonoBehaviour
{
    [SerializeField] private UIEquipmentSlot[] slots;

    private PlayerManager playerManager;
    private EquipmentsController playerEquipments;

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
        playerEquipments = playerActor.Actor.Equipments;

        foreach (var slot in slots)
        {
            slot.SetController(playerEquipments);
        }
    }
}
