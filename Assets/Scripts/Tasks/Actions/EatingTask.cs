using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class EatingTask : ActionTask
	{
        private AnimationController animationController;
        private float eatingTime;

        protected override void OnExecute()
        {
            eatingTime = 0f;
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