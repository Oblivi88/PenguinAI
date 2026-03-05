using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

    public class NavigationTask : ActionTask
    {
        public BBParameter<Vector3> targetPositionBBP;
        public BBParameter<float> timeSinceLastSampleBBP;
        public BBParameter<bool> isMovingBBP;

        public float sampleRateInSeconds;
        public float sampleRadiusInUnits;

        private Vector3 lastTargetPosition;
        private NavMeshAgent navAgent;

        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return $"{agent.name} - NavigationTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }

        protected override void OnUpdate()
        {
            timeSinceLastSampleBBP.value += Time.deltaTime;
            if (timeSinceLastSampleBBP.value > sampleRateInSeconds)
            {
                timeSinceLastSampleBBP.value = 0;

                if (lastTargetPosition != targetPositionBBP.value) // if the destination is different since last update
                {
                    lastTargetPosition = targetPositionBBP.value;
                    if (NavMesh.SamplePosition(targetPositionBBP.value, out NavMeshHit hitInfo, sampleRadiusInUnits, NavMesh.AllAreas))
                    {
                        navAgent.SetDestination(hitInfo.position);
                    }
                }

                isMovingBBP.value = navAgent.remainingDistance != 0 && navAgent.remainingDistance != Mathf.Infinity || navAgent.pathPending;
            }
        }
    }
}