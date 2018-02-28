using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Decision", menuName = "AI/Decisions/AnimationDoneDecision")]
public class AnimationDoneDecision : Decision {

	public string AnimationName;
	public string AnimationParamName;

	public override bool Decide (StateController controller)
	{
		bool isAnimDone = IsAnimationDone (controller);
		if (isAnimDone) {
			Debug.Log ("Anim is done");
			//controller.animator.SetBool (AnimationParamName, false);
		}else
			Debug.Log("Anim is not done");
		return isAnimDone;
	}

	bool IsAnimationDone (StateController controller)
	{
		return (controller.animator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1 && controller.animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName));
	}
}