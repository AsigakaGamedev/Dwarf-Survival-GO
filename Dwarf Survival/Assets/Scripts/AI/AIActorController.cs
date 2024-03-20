using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActorController : MonoBehaviour
{
    [SerializeField] private Actor actor;

    [Space]
    [SerializeField] private AIActorState[] allStates;

    [Space]
    [ReadOnly, SerializeField] private AIActorState currentState;

    private void Start()
    {
        actor.OnInitialize();
        actor.ChangeMovementType(false);

        foreach (AIActorState state in allStates)
        {
            state.OnInit(actor);
        }
    }

    private void OnDestroy()
    {
        actor.OnDeinitialize();
    }

    private void Update()
    {
        foreach (AIActorState state in allStates)
        {
            if (state.CanEnterState())
            {
                if (currentState)
                {
                    if (currentState == state)
                    {
                        break;
                    }
                    else
                    {
                        currentState.OnExitState();
                    }
                }

                currentState = state;
                currentState.OnEnterState();
                break;
            }
        }

        currentState.OnUpdateState();
    }
}
