using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Attack : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 1f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        Health target;
        float attackCD = 1f ;

        private void Update()
        {
            attackCD += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (IsInAttackRange())
            {
                GetComponent<Mover>().MoveToDirection(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            this.transform.LookAt(target.transform);
            if (attackCD > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                GetComponent<Animator>().ResetTrigger("stopAttack");
                attackCD = 0;
            }
        }

        public bool CanAttack()
        {
            return !target.IsDead();
        }

        void Hit()
        {
            target.takeDamage(weaponDamage);
        }

        private bool IsInAttackRange()
        {
            return Vector3.Distance(this.transform.position, target.transform.position) > weaponRange;
        }

        public void AttackTarget(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }
    }
}