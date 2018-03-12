using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Transform VineBridge;

	public bool IsActivated
	{
		get { return isActivated; }
	}

	private bool isActivated;
	private bool hasSpawnedPlatform;


	// Use this for initialization
	void Start()
	{
        VineBridge = this.gameObject.transform.GetChild(0);
        Debug.Log(VineBridge.name);
        VineBridge.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update()
	{
		if (!isActivated)
			return;

		if (!hasSpawnedPlatform)
		{
            //Spawn the plant here
            VineBridge.gameObject.SetActive(true);
            hasSpawnedPlatform = true;
			Debug.Log("Im hit with water");
		}

	}

	public void ActivatePlant()
	{
		isActivated = true;
	}
}
