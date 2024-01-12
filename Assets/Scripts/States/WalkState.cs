using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseState<PlayerStateMachine.PlayerState>
{
    public WalkState() : base(PlayerStateMachine.PlayerState.Walk)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Entered Walk State");
    }

    public override void ExitState()
    {
        Debug.Log("Exited Walk State");
    }

    public override void UpdateState()
    {
        Debug.Log("Walking");
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
