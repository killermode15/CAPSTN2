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

	public void TurnOnOrb(string tag)
	{
		Debug.Log("On");
		GameObject.FindGameObjectWithTag(tag).GetComponent<ParticleSystem>().Play();
		//GetComponentInParent<Absorb>().TurnOnOrb();
	}

	public void TurnOffOrb()
	{
		Debug.Log("Off");
		//GetComponentInParent<Absorb>().TurnOffOrb();
	}
}
