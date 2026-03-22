using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;

/*
MOVE TO TASK SCRIPT USED IN FSM
USED WHEN PENGUIN NEEDS TO MOVE TO ANOTHER TASK (EX. EATING, SLEEPING, CUDDLING)
*/
namespace NodeCanvas.Tasks.Actions
{

    public class MoveToTask : ActionTask
    {
        // reference to navMeshAgent
        private NavMeshAgent navAgent;
        // BBParemeter values for the player position, target task position, and chosen task value
        public BBParameter<Transform> playerBBP;
        public BBParameter<Vector3> targetTaskPositionBBP;
        public BBParameter<int> chosenTaskBBP;

        protected override string OnInit()
        {
            // on init, make sure reference to navMeshAgent is correct
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return $"{agent.name} - MoveToTask: Unable to get NavMesh Agent Reference!";
            }
            else
            {
                return null;
            }
        }

        protected override void OnExecute()
        {
            // every time node is activated, check what task needs satisfying.
            // 1-4 = wandering
            // 5 = sleeping
            // 6 = sliding
            // 7 = eating
            // 8 = cuddling

            // if wandering, immediately finish
            if (chosenTaskBBP.value < 5)
            {
                EndAction(true);
            }
            // if sleeping, find a random position to sleep
            else if (chosenTaskBBP.value == 5)
            {
                targetTaskPositionBBP.value = new Vector3(Random.Range(-30f, 30f), 3f, Random.Range(-30f, 30f));
            }
            // if sliding, immediately finish
            else if (chosenTaskBBP.value == 6)
            {
                EndAction(true);
            }
            // if eating, set destination to eating spot
            else if (chosenTaskBBP.value == 7)
            {
                targetTaskPositionBBP.value = new Vector3(-35.8f, 1.8f, 0f);
            }
            // set destination
            navAgent.SetDestination(targetTaskPositionBBP.value);
        }

        protected override void OnUpdate()
        {
            // if cuddling, set destination to player's position
            // (this is done in update instead because the players position is constantly updating)
            if (chosenTaskBBP.value == 8)
            {
                targetTaskPositionBBP.value = playerBBP.value.position;
                navAgent.SetDestination(targetTaskPositionBBP.value);
                if (navAgent.remainingDistance <= 1.5f) // if penguin reaches close to player, finish and start cuddling
                {
                    EndAction(true);
                }
            }
            // if task is anything other than cuddling, end the action when the AI has reached its target destination
            if (chosenTaskBBP.value < 8)
            {
                if (navAgent.remainingDistance <= 0.1f && !navAgent.pathPending)
                {
                    EndAction(true);
                }
            } 
        }
    }
}