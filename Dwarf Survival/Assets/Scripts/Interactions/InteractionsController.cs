using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayers;
    [SerializeField] private float interactRadius;

    [Space]
    [ReadOnly, SerializeField] private InteractableObject selectedInteractable;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

    public void CheckInteractions()
    {
        Collider2D[] collidersAround = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactLayers);

        if (collidersAround.Length > 0)
        {
            foreach (Collider2D collider in collidersAround)
            {
                if (!collider.isTrigger && collider.TryGetComponent(out InteractableObject newInteract))
                {
                    if (selectedInteractable)
                    {
                        if (selectedInteractable == newInteract)
                        {
                            return;
                        }

                        selectedInteractable.HideOutline();
                    }

                    selectedInteractable = newInteract;
                    selectedInteractable.ShowOutline();
                    return;
                }
            }
        }
        
        if (selectedInteractable)
        {
            selectedInteractable.HideOutline();
            selectedInteractable = null;
        }
    }

    public void TryInteract(Actor actor)
    {
        if (!selectedInteractable) return;

        selectedInteractable.Interact(actor);
        selectedInteractable.HideOutline();
        selectedInteractable = null;
    }
}
