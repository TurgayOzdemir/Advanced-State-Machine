using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new();
    public BaseState<EState> CurrentState { get; protected set; }

    private EState queuedState;
    private bool hasQueuedState = false;

    protected virtual void Start()
    {
        CurrentState.EnterState();
    }

    protected virtual void Update()
    {
        CurrentState.UpdateState();

        if (hasQueuedState)
        {
            TransitionToState(queuedState);
            hasQueuedState = false;
        }
    }

    protected void TransitionToState(EState stateKey)
    {
        if (CurrentState.StateKey.Equals(stateKey)) return;

        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
    }

    public void QueueNextState(EState stateKey)
    {
        queuedState = stateKey;
        hasQueuedState = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}
