﻿using System.Collections;
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
			hasSpawnedPlatform = true;
			//Spawn the plant here

		}

	}

	public void ActivatePlant()
	{
		isActivated = true;
	}
}
