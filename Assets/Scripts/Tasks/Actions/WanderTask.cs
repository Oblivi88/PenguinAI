using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    // a combination of the WanderTask and NavigationTask scripts from class, and some of my own needed additions
    public class WanderTask : ActionTask
    {
        public BBParameter<float> timeSinceLastSampleBBP;
        public BBParameter<Vector3> targetPositionBBP;
        public BBParameter<bool> isMovingBBP;

        public float sampleRateInSeconds;
        public float sampleRadiusInUnits;

        private Vector3 lastTargetPosition;
        private NavMeshAgent navAgent;

        public float wanderDistance = 4f;
        public float wanderRadius = 3f;

        public float wanderDurationTimer;
        public float wanderDurationMax;
        public BBParameter<int> chosenTaskBBP;

        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return $"{agent.name} - WanderTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }

        protected override void OnExecute()
        {
            wanderDurationTimer = 0f;
            wanderDurationMax = Random.Range(5f, 15f);

        }
        protected override void OnUpdate()
        {
            wanderDurationTimer += Time.deltaTime;
            if (wanderDurationTimer >= wanderDurationMax || chosenTaskBBP.value >= 5)
            {
                EndAction(true);
            }
            
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
            if (timeSinceLastSampleBBP.value == 0 && isMovingBBP.value == false)
            {
                Vector3 destination = CalculateTargetPosition();

                if (NavMesh.SamplePosition(destination, out NavMeshHit hitInfo, wanderDistance + wanderRadius, NavMesh.AllAreas))
                {
                    targetPositionBBP.value = hitInfo.position;
                }
            }

            
        }
        private Vector3 CalculateTargetPosition()
        {
            Vector3 circleCenter = agent.transform.position + agent.transform.forward * wanderDistance;
            Vector3 randomPoint = Random.insideUnitSphere.normalized * wanderRadius;

            Vector3 destination = circleCenter + randomPoint;

            return destination;
        }
    }
}