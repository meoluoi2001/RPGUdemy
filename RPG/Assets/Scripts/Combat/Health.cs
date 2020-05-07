using UnityEngine;
using System.Collections;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void takeDamage(float damage)
        {
            health -= damage;
        }
    }

    

}
