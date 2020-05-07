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

        private float attackSpeed = 1f;
        private ActionScheduler myActionScheduler;
        Transform target;
        IEnumerator attacking;
        private bool startedAnimation = false;
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
                    if (!startedAnimation)
                    {
                        attacking = startAttacking();
                        StartCoroutine(attacking);
                        startedAnimation = true;
                    }
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
            if (startedAnimation)
            {
                StopCoroutine(attacking);
                startedAnimation = false;
            }

            target = null;
        }

        IEnumerator startAttacking()
        {
            while (true)
            {
                GetComponent<Animator>().SetTrigger("Attacking");
                yield return new WaitForSeconds(1f/attackSpeed);
            }
        }

        // Animation Event
        void Hit()
        {

        }
    }
}
