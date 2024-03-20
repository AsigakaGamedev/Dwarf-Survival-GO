using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameObject outline;

    [Space]
    [SerializeField] private bool deactivateOnInteract;

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
}
