using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FSM
{
    public abstract class FSMBase<T> : MonoBehaviour where T : FSMBase<T>
    {
        private IStates<T> currentState;

        protected void StartState(IStates<T> starterState)
        {
            currentState = starterState;
            currentState.EnterState((T)this);
        }

        protected virtual void Update()
        {
            currentState.UpdateState((T)this);
        }

        public void SwitchState(IStates<T> nextState)
        {
            currentState = nextState;
            currentState.EnterState((T)this);
        }
    }
}

