using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State {

	public AnimationCurve chargeCurve;
	public float chargeDuration;
	public float chargeSpeed;
	public float chargeValue;
	private float initialChargeSpeed;
	private Vector3 playerGroundPosition;

	public override void OnEnable()
	{
		Debug.Log("starting attack:");
		if(Manager != null)
			playerGroundPosition = new Vector3 (Manager.Player.transform.position.x, transform.position.y, Manager.Player.transform.position.z);
		initialChargeSpeed = chargeDuration;
		chargeValue = chargeDuration;
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Debug.Log("attacking:");
		Charge ();
		if (Manager != null) {
			float distance = Vector3.Distance (transform.position, Manager.Player.transform.position);
			if (distance >= Manager.attackRange) {
				return false;
			}
		}

		return true;
	}

	void Charge(){
		chargeValue -= Time.deltaTime;
		float dash = (initialChargeSpeed == 0) ? 0 : chargeValue / initialChargeSpeed;
		Vector3 direction = playerGroundPosition - transform.position;
		transform.position = Vector3.MoveTowards(transform.position, direction * chargeSpeed, chargeCurve.Evaluate(dash));
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
