using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern.StateMachine;
using StatePattern.Main;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        
        public CloningState(GenericStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void OnStateEnter()
        {
            if ((Owner as CloneManController).cloneCount <= 0)
                return;

            CreateClone();
            CreateClone();
        }

        private void CreateClone()
        {
            CloneManController clone = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as CloneManController;
            clone.SetState(EnemyState.ACTIVE);
            clone.Agent.isStopped = false;
            clone.SetCloneCount((Owner as CloneManController).cloneCount - 1);
            clone.Teleport();
            clone.ChangeColor(Color.blue);
        }
        public void OnStateExit()
        {
        }

        public void Update()
        {
        }
    }
}
