using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Moment;
using RPG.Core;

namespace RPG.Combat
{
    public class Attack : MonoBehaviour, IAction
    {
        [SerializeField] private float attackRange = 2f;

        private ActionScheduler myActionScheduler;
        Transform target;
        private void Start()
        {
            myActionScheduler = GetComponent<ActionScheduler>();
        }
        void Update()
        {
            if (target != null)
            {
                if (Vector3.Distance(this.transform.position, target.position) > attackRange)
                {
                    GetComponent<Movement>().MoveToDirection(target.position);
                }
                else
                {
                    GetComponent<Movement>().Cancel();
                }
            }
        }

        public void AttackTarget(CombatTarget combatTarget)
        {
            myActionScheduler.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }
}


