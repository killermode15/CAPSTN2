using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	float bulletSpeed;
	[HideInInspector] public Vector3 direction;
	[HideInInspector] public bool isTargeted;
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

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Ground")) {
			Destroy (this.gameObject);
		}
	}

}
