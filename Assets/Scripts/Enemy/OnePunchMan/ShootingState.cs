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
    private PlayerController target;
    private float shootTimer;
    public ShootingState(OnePunchManStateMachine _stateMachine) => stateMachine = _stateMachine;

    public void OnStateEnter()
    {
        target = GameService.Instance.PlayerService.GetPlayer();
        shootTimer = 1f;
    }

    public void OnUpdate()
    {
        Quaternion finalRotation = CalculateRotation();
        Owner.SetRotation(Quaternion.RotateTowards(Owner.GetTransform().rotation, finalRotation, 1f));

        shootTimer -= Time.deltaTime/shootTimer;

        if(Mathf.Round(Quaternion.Angle(Owner.GetTransform().rotation, finalRotation)) == 0f)
        {
            if (Mathf.Round(shootTimer) <= 0)
            {
                Owner.Shoot();
                shootTimer = 1f;
            }
        }
    }

    private Quaternion CalculateRotation()
    {
        Vector3 vectorTowardsPlayer = target.Position - Owner.GetTransform().position;
        vectorTowardsPlayer.y = 0f;
        return Quaternion.LookRotation(vectorTowardsPlayer);
    }
    public void OnStateExit()
    {

    }
}
