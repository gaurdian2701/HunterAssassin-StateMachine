using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrolManStateMachine : IStateMachine
    {
        private EnemyController Owner;
        private IState currentState;
        protected Dictionary<States, IState> States = new Dictionary<States, IState>();
        public PatrolManStateMachine(EnemyController patrolManController)
        {
            Owner = patrolManController;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(Enemy.States.IDLE, new IdleState(this));
            States.Add(Enemy.States.ROTATING, new RotatingState(this));
            States.Add(Enemy.States.PATROLLING, new PatrollingState(this));
            States.Add(Enemy.States.CHASING, new ChasingState(this));   
            States.Add(Enemy.States.SHOOTING, new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach(IState state in States.Values)
                state.Owner = Owner;
        }
        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(States newState)
        {
            ChangeState(States[newState]);
        }

        public void Update() => currentState?.Update();
    }
}
