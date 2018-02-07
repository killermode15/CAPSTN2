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
	private float dash;
	private bool isDoneCharging;

	public override void OnEnable()
	{
		isDoneCharging = false;
		if (Manager != null)
			playerGroundPosition = new Vector3 (Manager.Player.transform.position.x, 0, 0);//transform.position.y, Manager.Player.transform.position.z);
		initialChargeSpeed = chargeDuration;
		chargeValue = chargeDuration;
		isDoneCharging = false;
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		if(!isDoneCharging)
			Charge ();
		if (Manager != null) {

			float distance = (playerGroundPosition - new Vector3 (transform.position.x, 0, 0)).magnitude;//Vector3.Distance (transform.position, Manager.Player.transform.position);
			if (isDoneCharging) {
				Debug.Log ("its da true");
				//Vector3 moveAway = transform.position - Manager.Player.transform.position;
				//moveAway.Normalize ();
				//transform.position = Vector3.MoveTowards (transform.position, moveAway * 10, 3 * Time.deltaTime);
				//return false;
			}

			if (distance >= Manager.attackRange) {
				return false;
			}
		}
		return true;
	}

	void Charge(){
		Debug.Log ("charging");
		chargeValue -= Time.deltaTime;
		dash = (initialChargeSpeed == 0) ? 0 : chargeValue / initialChargeSpeed;
		Vector3 direction = playerGroundPosition - new Vector3 (transform.position.x, 0, 0);// transform.position.z);
		direction.Normalize ();
		transform.position = Vector3.MoveTowards(transform.position, direction * chargeSpeed, chargeCurve.Evaluate(dash));
		if (dash <= 0) {
			//transform.position.x = Manager.Player.transform.position.x - Manager.DetectionRange;
			dash = 1;
			isDoneCharging = true;
		}
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
