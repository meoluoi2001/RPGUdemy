using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction currentAction;
        public void StartAction(IAction action)
        {
            if (currentAction != null && currentAction != action)
            {
                currentAction.Cancel();
                print("I am cancelling " + currentAction.ToString());
            }
            currentAction = action;
        }
    }
}
