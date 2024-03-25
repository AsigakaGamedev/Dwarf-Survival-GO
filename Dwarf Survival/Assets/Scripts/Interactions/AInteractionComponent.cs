using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(InteractableObject))]
public abstract class AInteractionComponent : MonoBehaviour
{
    private void OnValidate()
    {
        if (TryGetComponent(out InteractableObject interactableObject))
        {
            interactableObject.AddComponent(this);
        }  
    }

    public abstract void OnInteract(Actor actor);
}
