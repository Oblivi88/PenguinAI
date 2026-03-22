using NodeCanvas.Framework;
using UnityEngine;

/*
IDLE SCRIPT USED IN FSM
*/
namespace NodeCanvas.Tasks.Actions {
	public class IdleTask : ActionTask {
		// float to count how long penguin has been idling, and to determine how long it will idle for
		public float idleDuration;
		public float maxIdleDuration;
		// BBParameter value to set the chosen task
		// this will change the outcome of moveToTask script
        public BBParameter<int> chosenTaskBBP;

        protected override void OnExecute() {
			// every time node activates, set the timer back to 0, and set the max time to a random value
            idleDuration = 0f;
            maxIdleDuration = Random.Range(5f, 15f);
        }

		protected override void OnUpdate() {
			// incremment timer, if timer reaches max, choose a random task from 1-8, and end action.
			idleDuration += Time.deltaTime;
			if (idleDuration >= maxIdleDuration)
			{
				chosenTaskBBP.value = Random.Range(1, 9);
                EndAction(true);
			}
		}
    }
}