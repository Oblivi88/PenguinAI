using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class EatingTask : ActionTask
	{
        private NavMeshAgent navAgent;
        private AnimationController animationController;
        private float eatingTime;

        protected override string OnInit()
        {
            animationController = agent.GetComponent<AnimationController>();
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return $"{agent.name} - EatingTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }
        protected override void OnExecute()
        {
            eatingTime = 0f;
            animationController.isEating = true;
        }

        protected override void OnUpdate()
        {
            eatingTime += Time.deltaTime;
            if (eatingTime >= 6.5f)
            {
                animationController.isEating = false;
                EndAction(true);
            }
        }

    }
}