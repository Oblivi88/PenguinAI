using NodeCanvas.Framework;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class IdleTask : ActionTask {

		public float idleDuration;
		public float maxIdleDuration;

		protected override void OnExecute() {
            idleDuration = 0f;
            maxIdleDuration = Random.Range(5f, 15f);
			Debug.Log(maxIdleDuration);
        }

		protected override void OnUpdate() {
			idleDuration += Time.deltaTime;
			if (idleDuration >= maxIdleDuration)
			{
				Debug.Log("idle done");
				EndAction(true);
			}
		}
	}
}