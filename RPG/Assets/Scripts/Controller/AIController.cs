using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;


namespace RPG.Controller
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float enemySpeed = 1f;

        private float attackRange = 1f;

        private GameObject player;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < chaseDistance)
            {
                if (Vector3.Distance(this.transform.position, player.transform.position) < attackRange)
                {
                    AttackPlayer();
                } 
                else
                {
                    GetComponent<Attack>().Cancel();
                    ChasePlayer();
                }
                
            }
        }

        private void AttackPlayer()
        {
            if (player.GetComponent<Health>().IsDead() == true) return;
            GetComponent<Attack>().AttackTarget(player);
        }

        private void ChasePlayer()
        {
            GetComponent<Mover>().MoveToDirection(player.transform.position);
        }
    }
}
