using System.Collections;
using System.Collections.Generic;
//Script
using RPG.Moment;
using RPG.Combat;

// Unity
using UnityEngine;
using System;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {


        //Cached Component
        private Movement movementScript;
        private Attack attackScript;

        void Start()
        {
            attackScript = GetComponent<Attack>();
            movementScript = GetComponent<Movement>();
        }

        // Update is called once per frame
        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;            
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<CombatTarget>() != null && hit.transform.GetComponent<CombatTarget>().enabled != false)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        attackScript.AttackTarget(hit.transform.GetComponent<CombatTarget>());
                    }
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            Ray lastRay = GetRay();
            RaycastHit hitRay;
            bool hasHit = Physics.Raycast(lastRay, out hitRay);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    movementScript.StartMovement(hitRay.point);
                }
                
                return true;
            }
            return false;

        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}



