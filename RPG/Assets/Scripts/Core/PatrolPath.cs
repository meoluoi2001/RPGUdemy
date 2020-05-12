using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawWireSphere(GetWayPoint(i), 0.3f);
                Gizmos.DrawLine(GetWayPoint(i), NewMethod(i));
            }
        }

        public Vector3 NewMethod(int i)
        {
            return GetWayPoint(i < transform.childCount - 1 ? i + 1 : 0);
        }

        public Vector3 GetWayPoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}
