using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class WalkState : PlayerStates
    {
        public override void EnterState(PlayerFSM fsm)
        {
            Debug.Log("entered walk");
        }

        public override void UpdateState(PlayerFSM fsm)
        {
            if(fsm.executingNpcState == ExecutingNpcState.WALK)
            {
                Debug.Log("walking");
            }
            else
                ExitState(fsm);
        }

        public override void ExitState(PlayerFSM fsm)
        {
            Debug.Log("exited walk");
            if(fsm.executingNpcState == ExecutingNpcState.IDLE)
                fsm.SwitchState(fsm.idleState);
        }
    }
}

