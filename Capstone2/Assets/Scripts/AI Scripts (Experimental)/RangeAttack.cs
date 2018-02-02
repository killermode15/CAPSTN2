using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State {

	public GameObject projectile;
	public float attackSpeed;
	float Timer;
	public bool isRight;

	public float damping;
	float rotateTo;
	float turnSmoothVel;

	public override void OnEnable()
	{
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		LookAtPlayer ();
		Timer += attackSpeed * Time.deltaTime;
		if (Timer >= attackSpeed) {
			//shoot
			projectile.GetComponent<Projectile>().isTargeted = false;
			if (isRight) {
				projectile.GetComponent<Projectile> ().direction = Vector3.right;
			} else if (!isRight) {
				projectile.GetComponent<Projectile> ().direction = -Vector3.right;
			}
			Instantiate (projectile, transform.Find ("ShootPosition").position, transform.rotation);
			Timer = 0.0f;
		}
		//float distance = Vector3.Distance (transform.position, RangeManager.Player.transform.position);
		float distance = Vector3.Distance (transform.position, Manager.Player.transform.position);
		if (distance >= Manager.DetectionRange) {
			return false;
		}
		return true;
	}

	void LookAtPlayer(){
		Vector3 rotation = (Manager.Player.transform.position - transform.position);
		rotation.Normalize ();
		rotateTo = Mathf.Atan2 (rotation.x, rotation.y) * Mathf.Rad2Deg;
		if (transform.eulerAngles.z != rotateTo) {
			transform.eulerAngles = 
				new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			//transform.eulerAngles = Vector3.right * Mathf.SmoothDampAngle (transform.eulerAngles.x, rotateTo, ref turnSmoothVel, damping);
		}

		//Transform.rotation = new Vector3 (-180, -90, 90);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-180, -90, 90), Time.deltaTime * damping);
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
