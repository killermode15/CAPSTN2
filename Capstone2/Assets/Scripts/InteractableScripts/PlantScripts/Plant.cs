using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

	public bool IsActivated
	{
		get { return isActivated; }
	}

	private bool isActivated;
	private bool hasSpawnedPlatform;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (!isActivated)
			return;

		if (!hasSpawnedPlatform)
		{
			//Spawn the plant here
			hasSpawnedPlatform = true;
			Debug.Log("Im hit with water");
		}

	}

	public void ActivatePlant()
	{
		isActivated = true;
	}
}
