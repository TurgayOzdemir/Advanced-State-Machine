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
    public class PlayerFSM : FSMBase<PlayerFSM>
    {
        #region FSM
        public ExecutingNpcState executingNpcState;
    
        public IdleState idleState = new IdleState();
        public WalkState walkState = new WalkState();
        #endregion

        #region Observer
        void OnEnable()
        {
            EventManager.OnPlayerIdle.AddListener(() => executingNpcState = ExecutingNpcState.IDLE);
            EventManager.OnPlayerWalk.AddListener(() => executingNpcState = ExecutingNpcState.WALK);
        }
        void OnDisable()
        {
            EventManager.OnPlayerIdle.RemoveListener(() => executingNpcState = ExecutingNpcState.IDLE);
            EventManager.OnPlayerWalk.RemoveListener(() => executingNpcState = ExecutingNpcState.WALK);
        }
        #endregion
        
        public void Start()
        {
            executingNpcState = ExecutingNpcState.IDLE;
    
            StartState(idleState);
        }

        protected override void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                executingNpcState = ExecutingNpcState.WALK;
            }
            else
            {
                executingNpcState = ExecutingNpcState.IDLE;
            }

            base.Update();
        }
    }
}
