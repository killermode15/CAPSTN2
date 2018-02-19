using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunnedState : State {

	bool isStunned;
	[HideInInspector] public float stunnedDuration;

	public override void OnEnable()
	{
		if(GetComponent<NavMeshAgent> () != null)
			GetComponent<NavMeshAgent> ().isStopped = true;
		
		isStunned = true;
		StartCoroutine(Wait());
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Debug.Log ("OnUpdate Stunned State");
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
		if(GetComponent<NavMeshAgent> () != null)
			GetComponent<NavMeshAgent> ().isStopped = false;
		base.OnDisable();
	}

	IEnumerator Wait()
	{
		isStunned = true;
		yield return new WaitForSeconds(stunnedDuration);
		isStunned = false;
	}
}
