using StatePattern.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingState : IState
{
    public EnemyController Owner { get; set; }
    private IStateMachine stateMachine;
    private int currentPatrollingIndex = -1;
    private Vector3 currentDestination;

    public PatrollingState(IStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void OnStateEnter()
    {
        GetNextPatrollingIndex();
        currentDestination = Owner.Data.PatrollingPoints[currentPatrollingIndex];
        MoveTowardsDestination(currentDestination);
    }

    private void MoveTowardsDestination(Vector3 currentDestination)
    {
        Owner.Agent.destination = currentDestination;
        Owner.Agent.isStopped = false;
    }

    public void Update()
    {
        if (ReachedCurrentDestination())
            stateMachine.ChangeState(States.IDLE);
    }

    private bool ReachedCurrentDestination()
    {
        if(Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance)
            return true;
        return false;
    }

    private void GetNextPatrollingIndex()
    {
        if(currentPatrollingIndex == Owner.Data.PatrollingPoints.Count)
            currentPatrollingIndex = 0;
        else
            currentPatrollingIndex++;
    }

    public void OnStateExit()
    {
        Owner.Agent.isStopped = true;
    }
}
