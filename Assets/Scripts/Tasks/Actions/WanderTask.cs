using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    public class WanderTask : ActionTask
    {
        public BBParameter<float> timeSinceLastSampleBBP;
        public BBParameter<Vector3> targetPositionBBP;
        public BBParameter<bool> isMovingBBP;

        public float wanderDistance = 4f;
        public float wanderRadius = 3f;

        protected override void OnUpdate()
        {
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