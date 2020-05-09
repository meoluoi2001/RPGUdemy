using UnityEngine;
using System.Collections;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void takeDamage(float damage)
        {
            if (health == 0)
            {
                return;
            }
            if (health <= damage)
            {
                health = 0;
                GetComponent<Animator>().SetTrigger("isDeath");
            }
            else
            {
                health -= damage;
                print(health);
            }

        }
    }

    

}
