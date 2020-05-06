using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Moment
{
    public class Movement : MonoBehaviour, IAction
    {
        //Config




        private Animator myCharactorAnimator;
        private NavMeshAgent myMeshAgent;
        private ActionScheduler myActionScheduler;

        // Start is called before the first frame update
        void Start()
        {
            myActionScheduler = GetComponent<ActionScheduler>();
            myCharactorAnimator = GetComponent<Animator>();
            myMeshAgent = GetComponent<NavMeshAgent>();
        }


        void Update()
        {
            UpdateAnimation();
        }

        public void StartMovement(Vector3 destination)
        {
            myActionScheduler.StartAction(this);
            MoveToDirection(destination);
        }

        public void MoveToDirection(Vector3 destination)
        {

            myMeshAgent.isStopped = false;
            myMeshAgent.destination = destination;
        }

        public void Cancel()
        {
            myMeshAgent.isStopped = true;
        }

        private void UpdateAnimation()
        {

            Vector3 localVelocity = transform.InverseTransformDirection(myMeshAgent.velocity);
            myCharactorAnimator.SetFloat("MoveState", Mathf.Abs(localVelocity.z));
        }
       

    }

}

