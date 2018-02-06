using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State {

	public GameObject projectile;
	public float attackSpeed;
	float Timer;
	public bool isRight;
	float rotateTo;
	public float damping;
	float turnSmoothVel;

	private bool canShoot;

	public override void OnEnable()
	{
		base.OnEnable();
		canShoot = true;
	}

	public override bool OnUpdate()
	{
		if (canShoot) {
			LookAtPlayer ();
			Timer += attackSpeed * Time.deltaTime;
			if (Timer >= attackSpeed) {
				//shoot
				projectile.GetComponent<Projectile> ().isTargeted = true;
				/*if (isRight) {
				projectile.GetComponent<Projectile> ().direction = Vector3.right;
			} else if (!isRight) {
				projectile.GetComponent<Projectile> ().direction = -Vector3.forward;
			}*/
				projectile.GetComponent<Projectile> ().direction = Manager.Player.transform.position;
				Instantiate (projectile, transform.Find ("ShootPosition").position, transform.rotation);
				Timer = 0.0f;
			}
			float distance = Vector3.Distance (transform.position, Manager.Player.transform.position);
			if (distance >= Manager.DetectionRange) {
				return false;
			}
		}
		return true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		canShoot = false;
	}

	void LookAtPlayer(){
		Vector3 rotation = (Manager.Player.transform.position - transform.position);
		rotation.Normalize ();
		rotateTo = Mathf.Atan2(rotation.y , rotation.x) * Mathf.Rad2Deg;
		if (transform.eulerAngles.z != rotateTo) {
			transform.eulerAngles = Vector3.forward * Mathf.SmoothDampAngle (transform.eulerAngles.z, rotateTo, ref turnSmoothVel, damping);
		}
	}

	/*void LookRightOnly(){
		//Transform.rotation = new Vector3 (-180, -90, 90);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(-180, -90, 90), Time.deltaTime * damping);
		Debug.Log (transform.eulerAngles.x);
		if (transform.eulerAngles.x >= 355.0f || transform.eulerAngles.x >= -0.1f)
			ChangeState (GetState ("RangeAttack"));
	}*/
}
