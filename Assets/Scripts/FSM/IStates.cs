using UnityEngine;

namespace FSM
{
    public interface IStates<T> where T : MonoBehaviour 
    {
        void EnterState(T fsm);
        void UpdateState(T fsm);
        void ExitState(T fsm);
    }
}

