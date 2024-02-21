using StatePattern.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingState : IState
{
    public OnePunchManController Owner { get; set; }

    private OnePunchManStateMachine stateMachine;
    private Transform ownerTransform;
    private float targetAngle;

    public RotatingState(OnePunchManStateMachine _stateMachine) => stateMachine = _stateMachine;

    public void OnStateEnter()
    {
        ownerTransform = Owner.GetTransform();
        targetAngle = (ownerTransform.rotation.eulerAngles.y + 180) % 360;
    }

    public void OnUpdate()
    {
        Owner.SetRotation(CalculateRotation());
        if (RotationComplete())
            stateMachine.ChangeState(OnePunchManState.IDLE);
    }

    public void OnStateExit()
    {
        targetAngle = 0;
    }

    private Vector3 CalculateRotation()
        => Vector3.up * Mathf.MoveTowardsAngle(ownerTransform.rotation.eulerAngles.y, targetAngle, 3f * Time.deltaTime);

    private bool RotationComplete() => Mathf.Abs(Mathf.Abs(ownerTransform.rotation.eulerAngles.y) - Mathf.Abs(targetAngle)) == 0;
}
