using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State {

	public float MoveAwaySpeed;
	public float ChargeSpeed;
	public float ChargeDuration;
	public AnimationCurve ChargeCurve;

	private float chargeValue;
	private float initialChargeValue ;
	private bool doneCharging;
	private Vector3 chargeDir;
	

	public override void OnEnable()
	{
		base.OnEnable();
		if (ChargeDuration <= 0)
			ChargeDuration = 1;
		chargeValue = ChargeDuration;
		initialChargeValue = ChargeDuration;
		doneCharging = false;

		chargeDir = (new Vector3(Manager.Player.transform.position.x, 0, 0) - new Vector3(transform.position.x, 0, 0)).normalized;

	}

	public override bool OnUpdate()
	{
		if (!doneCharging)
		{
			Charge();
		}
		else
		{
			transform.position += chargeDir * MoveAwaySpeed * Time.deltaTime;
			if(Vector3.Distance(transform.position, Manager.Player.transform.position) > Manager.attackRange)
			{
				return false;
			}
		}

		return true;
	}

	void Charge(){
		chargeValue -= Time.deltaTime;
		float currentDashValue = chargeValue / initialChargeValue;
		//transform.position = Vector3.Lerp(transform.position, dir * (ChargeCurve.Evaluate(currentDashValue) * ChargeSpeed), currentDashValue);
		transform.position += chargeDir * (ChargeCurve.Evaluate(currentDashValue) * ChargeSpeed);

		if (currentDashValue <= 0)
			doneCharging = true;
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1.5f);
	}

	public override void OnDisable()
	{
		base.OnDisable();
		chargeDir = Vector3.zero;
		doneCharging = false;
	}
}
