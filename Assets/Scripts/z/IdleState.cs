using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class IdleState : NpcStates
    {
        public override void EnterState(NpcFSM fsm)
        {
            Debug.Log("entered idle");
        }

        public override void UpdateState(NpcFSM fsm)
        {
            if(fsm.executingNpcState == ExecutingNpcState.IDLE)
            {
                Debug.Log("idling");
            }
            else
                ExitState(fsm);
        }

        public override void ExitState(NpcFSM fsm)
        {
            Debug.Log("exited idle");
            if(fsm.executingNpcState == ExecutingNpcState.WALK)
                fsm.SwitchState(fsm.walkState);
        }
    }
}

