using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Actions/Club Smash Action")]
public class TrollClubSmashAction : Action {

	public override void Act (StateController controller)
	{
		Smash (controller);
	}

	void Smash(StateController controller){
		controller.attackScript.ClubSmash (controller);
	}
}
