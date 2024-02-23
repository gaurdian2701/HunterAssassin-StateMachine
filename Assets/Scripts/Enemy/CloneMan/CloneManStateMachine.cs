using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloneManStateMachine : GenericStateMachine<CloneManController>
    {

        public CloneManStateMachine(CloneManController Owner) : base(Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(StateMachine.States.IDLE, new IdleState<CloneManController>(this));
            States.Add(StateMachine.States.PATROLLING, new PatrollingState<CloneManController>(this));
            States.Add(StateMachine.States.CHASING, new ChasingState<CloneManController>(this));
            States.Add(StateMachine.States.SHOOTING, new ShootingState<CloneManController>(this));
            States.Add(StateMachine.States.TELEPORTING, new TeleportingState<CloneManController>(this));
            States.Add(StateMachine.States.CLONING, new CloningState<CloneManController>(this));
        }
    }
}
