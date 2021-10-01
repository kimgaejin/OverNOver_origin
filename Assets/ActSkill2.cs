using UnityEngine;
using System.Collections;

public class ActSkill2 : StateMachineBehaviour {

	public string Scene;

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Scene.Length <= 0) {
			Destroy (this, 0f);
		}
	}
		
}
