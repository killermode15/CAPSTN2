using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShooting : MonoBehaviour {

	public GameObject projectile;
	public float attackSpeed;
	float Timer;
	public bool isRight;

	void Start()
	{
		
	}
	void Update()
	{
		//shoot
		if (GetComponent<PeaShooterManager> ().isPaused) {
			Timer -= Time.deltaTime;
			if (Timer <= 0) {
				projectile.GetComponent<Projectile> ().isTargeted = false;
				projectile.GetComponent<Projectile> ().direction = Vector3.down;
				Instantiate (projectile, transform.Find ("ShootPosition").position, transform.rotation);
				Timer = attackSpeed;
			}
		}
	}
}
