using NodeCanvas.Framework;
using UnityEngine.AI;
using UnityEngine;

/*
SLIDING TASK SCRIPT USED IN FSM
*/

namespace NodeCanvas.Tasks.Actions {

	public class SlidingTask : ActionTask
	{
        // references to navMeshAgent and animationController script
        private NavMeshAgent navAgent;
        private AnimationController animationController;
        // parameter that decides where it will slide to
        public BBParameter<Vector3> targetSlidePositionBBP;
        protected override string OnInit()
        {
            // on init, make sure reference to navMeshAgent is correct
            animationController = agent.GetComponent<AnimationController>();
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return $"{agent.name} - SlidingTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }
        protected override void OnExecute()
        {
            // every time node is activated, play animation and set destination
            animationController.isSliding = true;
            targetSlidePositionBBP.value = new Vector3(Random.Range(-30f, 30f), 3f, Random.Range(-30f, 30f));
            navAgent.SetDestination(targetSlidePositionBBP.value);
        }

        protected override void OnUpdate()
        {
            // if it reaches end point, complete task
            if (navAgent.remainingDistance <= 0.1f && !navAgent.pathPending)
            {
                animationController.isSliding = false;
                EndAction(true);
            }
        }
    }
}