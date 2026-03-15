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
            navAgent = agent.GetComponent<NavMeshAgent>();
            animationController = agent.GetComponent<AnimationController>();

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
            EatingTime = 0f;
            animationController.isEating = true;
        }

        protected override void OnUpdate()
        {
            eatingTime += Time.deltaTime;
            if (eatingTime >= 10f)
            {
                animationController.isEating = false;
                EndAction(true);
            }
        }

    }
}