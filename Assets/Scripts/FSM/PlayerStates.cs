using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class PlayerStates : IStates<PlayerFSM>
    {
        public abstract void EnterState(PlayerFSM fsm);
        public abstract void UpdateState(PlayerFSM fsm);
        public abstract void ExitState(PlayerFSM fsm);
    }
}

