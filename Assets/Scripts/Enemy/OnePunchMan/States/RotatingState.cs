using StatePattern.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingState : IState
{
    public OnePunchManController Owner { get; set; }

    private OnePunchManStateMachine stateMachine;
    private Transform ownerTransform;

    public RotatingState(OnePunchManStateMachine _stateMachine) => stateMachine = _stateMachine;

    public void OnStateEnter()
    {
        ownerTransform = Owner.enemyTransform;
        Owner.targetRotation = (ownerTransform.rotation.eulerAngles.y + 180) % 360;
    }

    public void OnUpdate()
    {
        Owner.SetRotation(Owner.CalculateRotation());
        if (Owner.IsRotationComplete())
            stateMachine.ChangeState(OnePunchManState.IDLE);
    }

    public void OnStateExit()
    {
        Owner.targetRotation = 0f;
    }
}
