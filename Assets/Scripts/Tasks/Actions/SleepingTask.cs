using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;

/*
SLEEPING TASK SCRIPT USED IN FSM
*/
namespace NodeCanvas.Tasks.Actions {

    public class SleepingTask : ActionTask
    {
        // references to navMeshAgent and animationController script
        private NavMeshAgent navAgent;
        private AnimationController animationController;
        // timer to determine how long penguin sleeps for
        private float sleepTime;
        private float maxSleepTime;
        protected override string OnInit()
        {
            // on init, make sure reference to navMeshAgent is correct
            animationController = agent.GetComponent<AnimationController>();
            navAgent = agent.GetComponent<NavMeshAgent>();

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
            // every time node begins, reset timer, set random ending time, and start animation
            sleepTime = 0f;
            maxSleepTime = Random.Range(5, 13);
            animationController.isSleeping = true;
        }

        protected override void OnUpdate()
        {
            // increment timer, if timer reaches ending time, end task.
            sleepTime += Time.deltaTime;
            if (sleepTime >= maxSleepTime)
            {
                animationController.isSleeping = false;
                EndAction(true);
            }
        }


    }
}