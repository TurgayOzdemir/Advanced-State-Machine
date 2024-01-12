using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class WalkState : NpcStates
    {
        public override void EnterState(NpcFSM fsm)
        {
            Debug.Log("entered walk");
        }

        public override void UpdateState(NpcFSM fsm)
        {
            if(fsm.executingNpcState == ExecutingNpcState.WALK)
            {
                Debug.Log("walking");
            }
            else
                ExitState(fsm);
        }

        public override void ExitState(NpcFSM fsm)
        {
            Debug.Log("exited walk");
            if(fsm.executingNpcState == ExecutingNpcState.IDLE)
                fsm.SwitchState(fsm.idleState);
        }
    }
}

