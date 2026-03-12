using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

    public class MoveToTask : ActionTask
    {

        private NavMeshAgent navAgent;

        private Vector3 targetTaskPosition;
        public BBParameter<bool> isMovingBBP;
        public BBParameter<int> chosenTaskBBP;
        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return $"{agent.name} - MoveToTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }

        protected override void OnExecute()
        {
            if (chosenTaskBBP.value < 5)
            {
                EndAction(true);
            }
            else if (chosenTaskBBP.value == 5)
            {
                targetTaskPosition = new Vector3(14f, 3f, -15f);
            }
            navAgent.SetDestination(targetTaskPosition);
            
        }

        protected override void OnUpdate()
        {
            isMovingBBP.value = navAgent.remainingDistance != 0 && navAgent.remainingDistance != Mathf.Infinity || navAgent.pathPending;

            if (navAgent.remainingDistance <= 0.1f && !navAgent.pathPending)
            {
                EndAction(true);
            }
        }
    }
}