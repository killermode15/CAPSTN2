using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State {

	public AnimationCurve chargeCurve;
	public float chargeDuration;
	public float chargeSpeed;
	public float chargeValue;
	private float initialChargeSpeed;

	public override void OnEnable()
	{
		Debug.Log("starting attack:");
		initialChargeSpeed = chargeDuration;
		chargeValue = chargeDuration;
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Debug.Log("attacking:");
		Charge ();
		float distance = Vector3.Distance (transform.position, Manager.Player.transform.position);
		if (distance >= Manager.attackRange) {
			return false;
		}
		return true;
	}

	void Charge(){
		chargeValue -= Time.deltaTime;
		float dash = (initialChargeSpeed == 0) ? 0 : chargeValue / initialChargeSpeed;
		transform.position = Vector3.MoveTowards(transform.position, Manager.Player.transform.position, chargeCurve.Evaluate(dash));
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1.5f);
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
