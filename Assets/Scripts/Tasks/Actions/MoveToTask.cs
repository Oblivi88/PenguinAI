using NodeCanvas.Framework;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

    public class MoveToTask : ActionTask
    {

        private NavMeshAgent navAgent;

        public BBParameter<Transform> playerBBP;
        public BBParameter<Vector3> targetTaskPositionBBP;
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
                targetTaskPositionBBP.value = new Vector3(Random.Range(-30f, 30f), 3f, Random.Range(-30f, 30f));
            }
            else if (chosenTaskBBP.value == 6)
            {
                EndAction(true);
            }
            else if (chosenTaskBBP.value == 7)
            {
                targetTaskPositionBBP.value = new Vector3(-35.8f, 1.8f, 0f);
            }
                navAgent.SetDestination(targetTaskPositionBBP.value);
        }

        protected override void OnUpdate()
        {
            if (chosenTaskBBP.value== 8)
            {
                targetTaskPositionBBP.value = playerBBP.value.position;
                navAgent.SetDestination(targetTaskPositionBBP.value);
            }
            if (chosenTaskBBP.value > 8)
            {
                isMovingBBP.value = navAgent.remainingDistance! < 0.1f && navAgent.remainingDistance != Mathf.Infinity || navAgent.pathPending;
                if (navAgent.remainingDistance <= 0.1f && !navAgent.pathPending)
                {
                    EndAction(true);
                }
            }
            else if (chosenTaskBBP.value == 8)
            {
                isMovingBBP.value = navAgent.remainingDistance! < 1f && navAgent.remainingDistance != Mathf.Infinity || navAgent.pathPending;
                if (navAgent.remainingDistance <= 1.5f)
                {
                    EndAction(true);
                }
            }
            

            
        }
    }
}