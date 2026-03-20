using NodeCanvas.Framework;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    public class CuddlingTask : ActionTask
    {
        private NavMeshAgent navAgent;
        private AnimationController animationController;
        private float cuddlingTime;

        protected override string OnInit()
        {
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
            cuddlingTime = 0f;
            animationController.isCuddling = true;
        }

        protected override void OnUpdate()
        {
            cuddlingTime += Time.deltaTime;
            if (cuddlingTime >= 10f)
            {
                animationController.isCuddling = false;
                EndAction(true);
            }
        }
    }
}