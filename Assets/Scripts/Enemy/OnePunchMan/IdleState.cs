using StatePattern.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public OnePunchManController Owner { get; set; }

    private OnePunchManStateMachine stateMachine;

    private float idleTime;

    public IdleState(OnePunchManStateMachine _stateMachine) => stateMachine = _stateMachine;
    public void OnStateEnter()
    {
        idleTime = 2f;
    }

    public void OnUpdate()
    {
        idleTime -= Mathf.Round(Time.deltaTime / idleTime);

        if (idleTime <= 0)
            stateMachine.ChangeState(OnePunchManState.ROTATING);
    }

    public void OnStateExit()
    {
        idleTime = 0f;
    }
}
