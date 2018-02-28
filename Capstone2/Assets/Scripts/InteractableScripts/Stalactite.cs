using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {

	//Note: I set the damage reduction to Player.HP script
	[HideInInspector] public bool isFalling = false;
	public float Damage;
	float speed = 10.0f;
	Transform manager;
	Vector3 initialPos;

	// Use this for initialization
	void Start () {
		isFalling = true;
		initialPos = transform.position;
		manager = transform.parent.parent.parent;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (manager.GetComponent<Level1BossManager> ().startFall) {
			StartCoroutine ("Fall");
			isFalling = true;
		}*/

		if (manager.GetComponent<Level1BossManager> ().startFall) {
			//if (isFalling) {
			StartCoroutine ("Fall");
			//}
		} else if (!manager.GetComponent<Level1BossManager> ().startFall) {
			Respawn ();
		}
	}

	IEnumerator Fall()
	{
		//if (isFalling) {
			transform.Translate (Vector3.down * speed * Time.deltaTime, Space.World);
		//}
		yield return null;
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player") || other.CompareTag ("Ground")) {
			gameObject.SetActive (false);
			Respawn ();
		}
	}

	void Respawn(){
		Debug.Log ("calling res");
		isFalling = false;
		transform.position = initialPos;
		gameObject.SetActive (true);
	}

}
