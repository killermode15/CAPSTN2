using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooting : MonoBehaviour {

	public GameObject projectile;
	public float attackSpeed;
	float Timer;
	public bool isRight;

	void Update()
	{
		//shoot
		Timer += attackSpeed * Time.deltaTime;
		if (Timer >= attackSpeed) {
			projectile.GetComponent<Projectile> ().isTargeted = false;
			projectile.GetComponent<Projectile> ().direction = Vector3.down;
			Instantiate (projectile, transform.Find ("ShootPosition").position, transform.rotation);
			Timer = 0.0f;
		}
	}
}
