using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour {

	public GameObject orb;
	
	public void Respawn()
	{
		GetComponentInParent<Respawn>().respawn();
		GetComponentInParent<PlayerController>().anim.SetBoolAnimParam("IsDead", false);
	}

	public void SetCanMove(int val)
	{
		//Debug.Log((val == 0) ? false : true);
		GetComponentInParent<PlayerController>().SetCanMove(val);//(val == 0) ? false : true);
	}

	public void TurnOnOrb(string tag)
	{
		Debug.Log("On");
		orb = GameObject.FindGameObjectWithTag(tag);
		orb.GetComponent<ParticleSystem>().Play(); 
		//GetComponentInParent<Absorb>().TurnOnOrb();
	}

	public void TurnOffOrb()
	{
		Debug.Log("Off");
		//GetComponentInParent<Absorb>().TurnOffOrb();
	}
}
