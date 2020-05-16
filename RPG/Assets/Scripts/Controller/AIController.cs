using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using UnityEngine.AI;

namespace RPG.Controller
{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float patrolSpeed = 1f;
        [SerializeField] float chasingSpeed = 4f;
        [SerializeField] PatrolPath patrolPath;

        private float attackRange = 1f;
        private int currentWaypoint = 0;

        private Vector3 guardLocation;
        private Quaternion guardRotation;

        private GameObject player;
        private float timeSinceLastSeenPlayer;
        private float timeSinceWaitPoint = Mathf.Infinity;
        private float timeToMove = 3f;

        // Start is called before the first frame update
        void Start()
        {
            timeSinceLastSeenPlayer = Mathf.Infinity;
            if (patrolPath == null)
            {
                guardLocation = transform.position;
            } else
            {
                guardLocation = patrolPath.GetWayPoint(currentWaypoint);
            }
            
            guardRotation = transform.rotation;
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Health>().IsDead() || player.GetComponent<Health>().IsDead()) return;

            if (DistanceFromPlayer() < chaseDistance)
            {
                timeSinceLastSeenPlayer = 0;
                AttackBahaviour();
            }
            else if (timeSinceLastSeenPlayer < timeToMove)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timeSinceLastSeenPlayer += Time.deltaTime;

        }

        private void AttackBahaviour()
        {
            GetComponent<NavMeshAgent>().speed = chasingSpeed;
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

        private float DistanceFromPlayer()
        {
            return Vector3.Distance(this.transform.position, player.transform.position);
        }

        private void PatrolBehaviour()
        {         
            GetComponent<NavMeshAgent>().speed = patrolSpeed;
            GetComponent<Mover>().MoveToDirection(guardLocation);

            if (Vector3.Distance(transform.position, guardLocation) < 1f)
            {
                if (patrolPath == null)
                {
                    transform.rotation = guardRotation;
                }
                if (GetComponent<NavMeshAgent>().velocity == Vector3.zero)
                {
                    timeSinceWaitPoint += Time.deltaTime;
                }
            }

            if (patrolPath != null && timeSinceWaitPoint > timeToMove)
            {
                timeSinceWaitPoint = 0;
                currentWaypoint = currentWaypoint < patrolPath.transform.childCount - 1 ? currentWaypoint + 1 : 0;
                guardLocation = patrolPath.GetWayPoint(currentWaypoint);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
