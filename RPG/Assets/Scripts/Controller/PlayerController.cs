using System.Collections;
using System.Collections.Generic;
//Script
using RPG.Movement;
using RPG.Combat;

// Unity
using UnityEngine;
using System;
using RPG.Core;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {


        //Cached Component
        private Mover movementScript;
        private Attack attackScript;

        void Start()
        {
            attackScript = GetComponent<Attack>();
            movementScript = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Health>().IsDead()) return;

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;            
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<CombatTarget>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.transform.GetComponent<Health>().IsDead() || hit.transform.GetComponent<CombatTarget>() == null) continue;
                        attackScript.AttackTarget(hit.transform.gameObject);
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



