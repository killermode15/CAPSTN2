using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 3);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.right * 10 * Time.deltaTime;
	}
}
