using NodeCanvas.Framework;
using UnityEngine.AI;
using UnityEngine;

/*
CUDDLING TASK SCRIPT USED IN FSM
PENGUIN WILL APPROACH PLAYER PENGUIN AND CUDDLE
*/
namespace NodeCanvas.Tasks.Actions
{

    public class CuddlingTask : ActionTask
    {
        // references top navMeshAgent and animationController script, and a float for cuddling duration
        private NavMeshAgent navAgent;
        private AnimationController animationController;
        private float cuddlingTime;

        protected override string OnInit()
        {
            // on init, make sure reference to navMeshAgent is correct
            animationController = agent.GetComponent<AnimationController>();
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return $"{agent.name} - CuddlingTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }
        protected override void OnExecute()
        {
            // every time node is activated, reset timer to 0, begin animation
            cuddlingTime = 0f;
            animationController.isCuddling = true;
        }

        protected override void OnUpdate()
        {
            // increment timer, if timer reaches max, stop task
            cuddlingTime += Time.deltaTime;
            if (cuddlingTime >= 10f)
            {
                animationController.isCuddling = false;
                EndAction(true);
            }
        }
    }
}