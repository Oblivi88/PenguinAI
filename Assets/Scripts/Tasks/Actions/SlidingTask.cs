using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SlidingTask : ActionTask
	{
        private NavMeshAgent navAgent;
        private AnimationController animationController;

        public BBParameter<Vector3> targetSlidePositionBBP;
        protected override string OnInit()
        {
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
            animationController.isSliding = true;
            targetSlidePositionBBP.value = new Vector3(Random.Range(-30f, 30f), 3f, Random.Range(-30f, 30f));
            navAgent.SetDestination(targetSlidePositionBBP.value);
        }

        protected override void OnUpdate()
        {
            if (navAgent.remainingDistance <= 0.1f && !navAgent.pathPending)
            {
                animationController.isSliding = false;
                EndAction(true);
            }
        }
    }
}