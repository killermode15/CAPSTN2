using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	float bulletSpeed;
	public Vector3 direction;
	public bool isTargeted;
	public float ProjectileLife;
	public float damage;

	// Use this for initialization
	void Start () {
		bulletSpeed = 7.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Shoot ();
		Destroy (this.gameObject, ProjectileLife);
	}

	void Shoot(){
		if (!isTargeted)
			transform.Translate (direction * bulletSpeed * Time.deltaTime);
		else if (isTargeted)
			transform.position = Vector3.MoveTowards (transform.position, transform.right * 50
				+ direction, Time.deltaTime * bulletSpeed);
	}
}
