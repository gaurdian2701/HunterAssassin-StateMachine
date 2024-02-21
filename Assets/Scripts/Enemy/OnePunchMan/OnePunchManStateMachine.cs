using StatePattern.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePunchManStateMachine
{
    private OnePunchManController Owner;

    protected Dictionary<OnePunchManState, IState> States = new Dictionary<OnePunchManState, IState>();

    private IState currentState;
    
    public OnePunchManStateMachine(OnePunchManController Owner)
    {
        this.Owner = Owner;
        CreateStates();
        SetOwner();
    }
    private void CreateStates()
    {
        States.Add(OnePunchManState.IDLE, new IdleState(this));
        States.Add(OnePunchManState.ROTATING, new RotatingState(this));
        States.Add(OnePunchManState.SHOOTING, new ShootingState(this));
    }

    private void SetOwner()
    {
        foreach(IState state in States.Values)
            state.Owner = Owner;    
    }

    public void Update()
    {
        currentState?.OnUpdate();
    }

    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void ChangeState(OnePunchManState newState) => ChangeState(States[newState]);
}
