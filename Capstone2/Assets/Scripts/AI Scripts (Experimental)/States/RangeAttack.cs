using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : State {

	public GameObject projectile;
	public float attackSpeed;
	float Timer;
	public bool isRight;

	public override void OnEnable()
	{
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Timer += attackSpeed * Time.deltaTime;
		if (Timer >= attackSpeed) {
			//shoot
			projectile.GetComponent<Projectile>().isTargeted = false;
			if (isRight) {
				projectile.GetComponent<Projectile> ().direction = Vector3.right;
			} else if (!isRight) {
				projectile.GetComponent<Projectile> ().direction = -Vector3.forward;
			}
			Instantiate (projectile, transform.Find ("ShootPosition").position, transform.rotation);
			Timer = 0.0f;
		}
		float distance = Vector3.Distance (transform.position, Manager.Player.transform.position);
		if (distance >= Manager.DetectionRange) {
			return false;
		}
		return false;
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
