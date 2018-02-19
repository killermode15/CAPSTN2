using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour {

	
	public void Respawn()
	{
		GetComponentInParent<Respawn>().respawn();
		GetComponentInParent<PlayerController>().anim.SetBoolAnimParam("IsDead", false);
	}

	public void SetCanMove(int val)
	{
		GetComponentInParent<PlayerController>().SetCanMove(val);
	}
}
