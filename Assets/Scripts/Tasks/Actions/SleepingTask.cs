using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

    public class SleepingTask : ActionTask
    {

        private NavMeshAgent navAgent;
        private AnimationController animationController;
        private float sleepTime;
        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();
            animationController = agent.GetComponent<AnimationController>();

            if (navAgent == null)
            {
                return $"{agent.name} - SleepingTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }

        protected override void OnExecute()
        {
            sleepTime = 0f;
            animationController.isSleeping = true;
        }

        protected override void OnUpdate()
        {
            sleepTime += Time.deltaTime;
            if (sleepTime >= 10f)
            {
                animationController.isSleeping = false;
                EndAction(true);
            }
        }


    }
}