using EPOOutline;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outlinable))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private Outlinable outline;

    [Space]
    [SerializeField] private bool deactivateOnInteract;

    [Space]
    [SerializeField] private List<AInteractionComponent> components = new List<AInteractionComponent>();

    private void OnValidate()
    {
        if (!outline) outline = GetComponent<Outlinable>();
    }

    private void Start()
    {
        HideOutline();
    }

    public void ShowOutline()
    {
        outline.enabled = true;
    }

    public void HideOutline()
    {
        outline.enabled = false;
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
