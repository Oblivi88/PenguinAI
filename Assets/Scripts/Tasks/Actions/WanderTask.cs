using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;

/*
WANDERING TASK SCRIPT USED IN FSM
WHEN NO OTHER TASKS ARE CHOSEN, PENGUIN WANDERS AROUND RANDOMLY
*/
namespace NodeCanvas.Tasks.Actions
{
    // a combination of the WanderTask and NavigationTask scripts from class, and some of my own needed additions
    public class WanderTask : ActionTask
    {
        // wandering BBParemeters and float values
        public BBParameter<float> timeSinceLastSampleBBP;
        public BBParameter<Vector3> targetPositionBBP;
        public BBParameter<bool> isMovingBBP;
        public float sampleRateInSeconds;
        public float sampleRadiusInUnits;
        public float wanderDistance = 4f;
        public float wanderRadius = 3f;
        private Vector3 lastTargetPosition;
        // reference to navMeshAgent
        private NavMeshAgent navAgent;
        // float values for how long penguin will wander for
        public float wanderDurationTimer;
        public float wanderDurationMax;
        // BBParemeter for which task the penguin needs to satisfy
        public BBParameter<int> chosenTaskBBP;

        protected override string OnInit()
        {
            // on init, make sure reference to navMeshAgent is correct
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
            // every time node activates, reset timer to 0, and set max time to a random value
            wanderDurationTimer = 0f;
            wanderDurationMax = Random.Range(5f, 15f);

        }
        protected override void OnUpdate()
        {
            // increment time, if timer reaches max OR the chosen task is not 1-4 (anything that isnt wandering), end the task.
            wanderDurationTimer += Time.deltaTime;
            if (wanderDurationTimer >= wanderDurationMax || chosenTaskBBP.value >= 5)
            {
                EndAction(true);
            }
            
            // wandering script from class
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
        // calculate random wander points within circle
        private Vector3 CalculateTargetPosition()
        {
            Vector3 circleCenter = agent.transform.position + agent.transform.forward * wanderDistance;
            Vector3 randomPoint = Random.insideUnitSphere.normalized * wanderRadius;

            Vector3 destination = circleCenter + randomPoint;

            return destination;
        }
    }
}