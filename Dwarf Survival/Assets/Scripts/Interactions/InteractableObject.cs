using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject outline;

    [Space]
    [SerializeField] private bool deactivateOnInteract;

    [Space]
    [SerializeField] private List<AInteractionComponent> components = new List<AInteractionComponent>();

    private void Start()
    {
        HideOutline();
    }

    public void ShowOutline()
    {
        if (outline) outline.SetActive(true);
    }

    public void HideOutline()
    {
        if (outline) outline.SetActive(false);
    }

    public void Interact(Actor actor)
    {
        foreach (AInteractionComponent component in components)
        {
            component.OnInteract(actor);
        }

        if (deactivateOnInteract) gameObject.SetActive(false);
    }

    public void AddComponent(AInteractionComponent newComp)
    {
        if (components.Contains(newComp)) return;   

        components.Add(newComp);
    }
}
