using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        bool death = false;
        public void takeDamage(float damage)
        {
            if (death)
            {
                return;
            }
            if (health <= damage)
            {
                health = 0;
                death = true;
                GetComponent<Animator>().SetTrigger("isDeath");
                GetComponent<ActionScheduler>().CancelCurrentAction();
/*                GetComponent<NavMeshAgent>().enabled = false;
*/            }
            else
            {
                health -= damage;
                print(health);
            }

        }

        public bool IsDead()
        {
            return death;
        }
    }

    

}
