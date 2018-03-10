using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarObject : MonoBehaviour
{
	public bool isActivated;
	public List<FloatingObject> FloatingObjects;
	public List<ParticleSystem> ParticleFX;

	private GameObject player;
	private bool isvfxFullActive;

	// Use this for initialization
	void Start()
	{
		isActivated = false;
	}

	// Update is called once per frame
	void Update()
	{

		if (player)
		{
			if (player.GetComponent<OrbAbsorb>().MaxOrbs == player.GetComponent<OrbAbsorb>().OrbCount && !isActivated)
			{
				if (InputManager.Instance.GetKey(ControllerInput.ActivateAltar))
				{
					ActivateFloatingObjects();
					transform.GetChild(0).GetComponent<DialogueTrigger>().TriggerDialogue();
					isActivated = true;
					player.GetComponent<OrbAbsorb>().OrbCount = 0;
				}
			}
		}
	}

	void ActivateFloatingObjects()
	{
		foreach(FloatingObject obj in FloatingObjects)
		{
			obj.ActivateFloatingObject();
		}
	}

	void ActivateParticleEffects()
	{
		foreach (ParticleSystem ps in ParticleFX)
		{
			ps.Play();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			player = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			player = null;
		}
	}
}
