using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour {

	public Vector2 ForceLimit;
	public float UpwardForce;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(new Vector3(GetRandomForce(ForceLimit), UpwardForce, GetRandomForce(ForceLimit)));	
	}

	private float GetRandomForce(Vector2 limit)
	{
		return Random.Range(ForceLimit.x, ForceLimit.y);
	}
}
