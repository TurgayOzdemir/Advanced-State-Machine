using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExecutingNpcState
{
    IDLE,
    WALK,
}

namespace FSM
{
    public class NpcFSM : MonoBehaviour
    {
        #region FSM
        public ExecutingNpcState executingNpcState;
    
        public NpcStates currentState;
        public IdleState idleState = new IdleState();
        public WalkState walkState = new WalkState();
        #endregion
    
        void Start()
        {
            executingNpcState = ExecutingNpcState.IDLE;
    
            currentState = idleState;
            currentState.EnterState(this);
        }
    
        void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                executingNpcState = ExecutingNpcState.WALK;
            }
            else
            {
                executingNpcState = ExecutingNpcState.IDLE;
            }

            currentState.UpdateState(this);
        }
    
        public void SwitchState(NpcStates nextState)
        {
            currentState = nextState;
            currentState.EnterState(this);
        }
    }
}
