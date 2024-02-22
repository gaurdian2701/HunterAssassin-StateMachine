using StatePattern.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public OnePunchManController Owner { get; set; }

    private OnePunchManStateMachine stateMachine;

    public IdleState(OnePunchManStateMachine _stateMachine) => stateMachine = _stateMachine;
    public void OnStateEnter()
    {
        Owner.ResetTimer();
    }

    public void OnUpdate()
    {
        Owner.idleTimer -= Time.deltaTime / Owner.idleTimer;

        if (Mathf.Round(Owner.idleTimer) <= 0)
            stateMachine.ChangeState(OnePunchManState.ROTATING);
    }

    public void OnStateExit()
    {
        Owner.ResetTimer();
    }
}
