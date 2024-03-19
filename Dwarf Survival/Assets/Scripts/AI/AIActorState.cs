using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIActorState : MonoBehaviour
{
    protected Actor actor;

    public virtual void OnInit(Actor actor) { this.actor = actor; }
    public virtual bool CanEnterState() {  return true; }

    public virtual void OnEnterState() { }
    public virtual void OnUpdateState() { }
    public virtual void OnExitState() { }
}
