using StatePattern.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class TeleportingState<T> : IState where T : EnemyController
    {
        private GenericStateMachine<T> stateMachine;
        public EnemyController Owner { get; set; }

        public TeleportingState(GenericStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void OnStateEnter()
        {
            TeleportToRandomPosition();
            stateMachine.ChangeState(States.CHASING);
        }

        private void TeleportToRandomPosition()
        {
            Owner.Agent.Warp(GetRandomPositionInGame());
        }

        private Vector3 GetRandomPositionInGame()
        {
            Vector3 pos = Random.insideUnitSphere * Owner.Data.RangeRadius + Owner.Position;
            NavMeshHit navMeshHit;

            if (NavMesh.SamplePosition(pos, out navMeshHit, Owner.Data.RangeRadius, NavMesh.AllAreas))
                return navMeshHit.position;

            return Owner.Data.SpawnPosition;
        }

        public void OnStateExit()
        {
        }

        public void Update()
        {
        }
    }
}
