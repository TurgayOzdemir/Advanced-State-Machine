using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class NpcStates
    {
        public abstract void EnterState(NpcFSM fsm);
        public abstract void UpdateState(NpcFSM fsm);
        public abstract void ExitState(NpcFSM fsm);
    }
}

