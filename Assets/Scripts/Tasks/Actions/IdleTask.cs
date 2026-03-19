using NodeCanvas.Framework;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class IdleTask : ActionTask {

		public float idleDuration;
		public float maxIdleDuration;
        public BBParameter<int> chosenTaskBBP;

        protected override void OnExecute() {
            idleDuration = 0f;
            maxIdleDuration = Random.Range(5f, 15f);
			Debug.Log(maxIdleDuration);
        }

		protected override void OnUpdate() {
			idleDuration += Time.deltaTime;
			if (idleDuration >= maxIdleDuration)
			{
				chosenTaskBBP.value = Random.Range(1, 9);
                EndAction(true);
			}
		}
    }
}