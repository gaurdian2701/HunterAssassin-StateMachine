using StatePattern.Player;
using StatePattern.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloneManController : EnemyController
    {
        private CloneManStateMachine stateMachine;
        public int cloneCount { get; private set; }

        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
            SetCloneCount(enemyScriptableObject.cloneCount);
        }

        private void CreateStateMachine() => stateMachine = new CloneManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }

        public override void Shoot()
        {
            base.Shoot();
            stateMachine.ChangeState(States.TELEPORTING);
        }

        public void ChangeColor(Color color)
        {
            enemyView.SetDefaultColor(color);
            enemyView.SetColor(color);
        }
        public void Teleport()
        {
            stateMachine.ChangeState(States.TELEPORTING);
        }

        public override void Die()
        {
            stateMachine.ChangeState(States.CLONING);
            base.Die();
        }
        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }

        public void SetCloneCount(int count) => cloneCount = count;
        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);
    }
}

