using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState<PlayerStateMachine.PlayerState>
{
    public IdleState() : base(PlayerStateMachine.PlayerState.Idle)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entered Idle State");
    }

    public override void ExitState()
    {
        Debug.Log("Exited Idle State");
    }

    public override void UpdateState()
    {
        Debug.Log("Idle");
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {

    }
}

