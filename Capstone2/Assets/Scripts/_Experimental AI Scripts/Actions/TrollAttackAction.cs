using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Action", menuName = "AI/Actions/Attack Action")]
public class TrollAttackAction : Action {

	public override void Act(StateController controller)
	{
		throw new NotImplementedException();
	}

	void Attack(StateController controller)
	{
		//This is where the troll attacks
		Debug.Log("I am attacking");
	}
}
