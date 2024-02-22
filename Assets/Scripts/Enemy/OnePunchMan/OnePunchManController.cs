using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;
using System;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private bool isIdle;
        private bool isRotating;
        private bool isShooting;
        public float idleTimer;
        public float shootTimer;
        public float targetRotation;
        public PlayerController target;
        public Transform enemyTransform;
        private OnePunchManStateMachine onePunchManStateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            enemyTransform = enemyView.transform;
            CreateStateMachine();
            InitializeVariables();
        }

        private void CreateStateMachine()
        {
            onePunchManStateMachine = new OnePunchManStateMachine(this);
            onePunchManStateMachine.ChangeState(OnePunchManState.IDLE);
        }

        private void InitializeVariables()
        {
            isIdle = true;
            isRotating = false;
            isShooting = false;
            idleTimer = enemyScriptableObject.IdleTime;
            shootTimer = enemyScriptableObject.RateOfFire;
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            onePunchManStateMachine.Update();
        }

        public void ResetTimer() => idleTimer = enemyScriptableObject.IdleTime;
        public void ResetShootTimer() => shootTimer = enemyScriptableObject.RateOfFire;

        public Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Rotation.eulerAngles.y, targetRotation, enemyScriptableObject.RotationSpeed * Time.deltaTime);

        public bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Data.RotationThreshold;

        public bool IsFacingPlayer(Quaternion desiredRotation)
        {
            return Quaternion.Angle(Rotation, desiredRotation) < Data.RotationThreshold;
        }

        public Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        
        public Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Rotation, desiredRotation, enemyScriptableObject.RotationSpeed/30 * Time.deltaTime);

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            isIdle = false;
            isRotating = false;
            isShooting = true;
            target = targetToSet;
            shootTimer = 0;

            onePunchManStateMachine.ChangeState(OnePunchManState.SHOOTING);
        }

        public override void PlayerExitedRange() 
        {
            onePunchManStateMachine.ChangeState(OnePunchManState.IDLE);
        }
    }
}