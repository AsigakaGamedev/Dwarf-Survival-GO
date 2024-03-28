using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAddInteract : AInteractionComponent
{
    [SerializeField] private BuffData buffData;

    public override void OnInteract(Actor actor)
    {
        actor.Buffs.AddBuff(buffData);
    }
}
