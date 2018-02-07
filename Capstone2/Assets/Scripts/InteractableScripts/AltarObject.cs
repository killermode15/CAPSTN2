using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarObject : MonoBehaviour {


	private GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.Instance.GetKeyDown(ControllerInput.ActivateAltar))
		{
			if (player)
			{
				CorruptionBar corruption = player.GetComponent<CorruptionBar>();

			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			player = other.gameObject;
		}
	}
}
