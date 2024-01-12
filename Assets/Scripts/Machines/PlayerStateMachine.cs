using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerStateMachine.PlayerState>
{
    public enum PlayerState
    {
        Idle,
        Walk
    }

    void Awake()
    {
        States.Add(PlayerState.Idle, new IdleState());
        States.Add(PlayerState.Walk, new WalkState());

        CurrentState = States[PlayerState.Idle];
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            QueueNextState(PlayerState.Walk);
        }
        else
        {
            QueueNextState(PlayerState.Idle);
        }

        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

}
