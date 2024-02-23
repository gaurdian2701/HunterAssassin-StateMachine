using StatePattern.Enemy;
using StatePattern.Main;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : IState
{
    public EnemyController Owner { get; set; }
    private IStateMachine stateMachine;
    private Vector3 playerDestination;

    public ChasingState(IStateMachine stateMachine) => this.stateMachine = stateMachine;
    public void OnStateEnter()
    {
        playerDestination = GameService.Instance.PlayerService.GetPlayer().Position;
        Owner.Agent.isStopped = false;
        Owner.Agent.destination = playerDestination;
    }

    public void Update()
    {
        if (EnemyHasReachedPlayer())
            stateMachine.ChangeState(States.SHOOTING);
    }

    private bool EnemyHasReachedPlayer()
    {
        if(Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance)
            return true;
        return false;
    }

    public void OnStateExit()
    {
        Owner.Agent.isStopped = true;
    }
}
