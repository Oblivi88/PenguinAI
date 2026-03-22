using NodeCanvas.Framework;
using UnityEngine.AI;
using UnityEngine;

/*
EATING TASK SCRIPT USED IN FSM
*/
namespace NodeCanvas.Tasks.Actions {

	public class EatingTask : ActionTask
	{
        // references to the navMeshAgent and animationControllerScript
        private NavMeshAgent navAgent;
        private AnimationController animationController;
        // a timer to determine how long penguin eats for
        private float eatingTime;

        protected override string OnInit()
        {
            // on init, make sure reference to navMeshAgent is correct
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
            // every time node is activated, reset timer, and start animation
            eatingTime = 0f;
            animationController.isEating = true;
        }

        protected override void OnUpdate()
        {
            // increment timer, once timer reaches set value, finish task (done eating).
            eatingTime += Time.deltaTime;
            if (eatingTime >= 6.5f)
            {
                animationController.isEating = false;
                EndAction(true);
            }
        }

    }
}