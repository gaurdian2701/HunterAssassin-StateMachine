using StatePattern.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : IState
{
    public OnePunchManController Owner { get; set; }

    private OnePunchManStateMachine stateMachine;
    public ShootingState(OnePunchManStateMachine _stateMachine) => stateMachine = _stateMachine;

    public void OnStateEnter()
    {
        
    }

    public void OnStateExit()
    {

    }

    public void OnUpdate()
    {
        Owner.Shoot();
    }
}
