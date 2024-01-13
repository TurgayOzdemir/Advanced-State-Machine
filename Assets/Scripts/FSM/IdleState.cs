using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class IdleState : PlayerStates
    {
        public override void EnterState(PlayerFSM fsm)
        {
            Debug.Log("entered idle");
        }

        public override void UpdateState(PlayerFSM fsm)
        {
            if(fsm.executingNpcState == ExecutingNpcState.IDLE)
            {
                Debug.Log("idling");
            }
            else
                ExitState(fsm);
        }

        public override void ExitState(PlayerFSM fsm)
        {
            Debug.Log("exited idle");
            if(fsm.executingNpcState == ExecutingNpcState.WALK)
                fsm.SwitchState(fsm.walkState);
        }
    }
}

