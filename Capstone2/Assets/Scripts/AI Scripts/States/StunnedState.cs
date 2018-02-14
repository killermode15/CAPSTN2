using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : State {

	bool isStunned;
	[HideInInspector] public float stunnedDuration;

	public override void OnEnable()
	{
		isStunned = true;
		StartCoroutine(Wait());
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Debug.Log ("AAUUGGHHFFSFS");
		if (this.gameObject.GetComponent<PeaShooting> () != null) {
			if (isStunned)
				this.gameObject.GetComponent<PeaShooting> ().enabled = false;
			else
				this.gameObject.GetComponent<PeaShooting> ().enabled = true;
		}
		if(!isStunned)
		{
			return false;
		}
		return true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}

	IEnumerator Wait()
	{
		isStunned = true;
		yield return new WaitForSeconds(stunnedDuration);
		isStunned = false;
	}
}
