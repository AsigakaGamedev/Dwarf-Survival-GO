using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AAttack : MonoBehaviour
{
    public virtual void OnInit() { }

    public abstract void OnAttack(Vector2 dir);
}
