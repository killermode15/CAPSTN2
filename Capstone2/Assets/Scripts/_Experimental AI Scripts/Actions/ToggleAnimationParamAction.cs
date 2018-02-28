using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Action", menuName = "AI/Actions/Toggle Animation Parameter Action")]
public class ToggleAnimationParamAction : Action {

	public string ParamName;
	public bool ParamState;

	public override void Act (StateController controller)
	{
		Toggle (controller);
	}

	void Toggle(StateController controller)
	{

		foreach (AnimatorControllerParameter param in controller.animator.parameters) {
			switch (param.type) {
			case AnimatorControllerParameterType.Bool:
				controller.animator.SetBool (param.name, false);
				break;
			}
		}

		Debug.Log ("Parameter: " + ParamName + " State: " + ParamState);
		controller.animator.SetBool (ParamName, ParamState);

	}
}
