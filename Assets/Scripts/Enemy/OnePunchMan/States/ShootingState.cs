using StatePattern.Enemy;
using StatePattern.Main;
using StatePattern.Player;
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
        Owner.target = GameService.Instance.PlayerService.GetPlayer();
    }

    public void OnUpdate()
    {
        Owner.SetRotation(Owner.CalculateRotationTowardsPlayer());
        Owner.shootTimer -= Time.deltaTime/Owner.shootTimer;

        if (Owner.shootTimer <= 0 && Owner.IsFacingPlayer(Owner.CalculateRotationTowardsPlayer()))
        {
            Owner.Shoot();
            Owner.ResetShootTimer();
        }
    }

    public void OnStateExit()
    {
        Owner.ResetShootTimer();
    }
}
