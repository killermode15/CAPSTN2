using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarObject : MonoBehaviour
{
	public bool isActivated;
	public List<FloatingObject> FloatingObjects;
	public List<ParticleSystem> ParticleFX;

	private GameObject player;
	private bool hasTriggeredEffects;

	// Use this for initialization
	void Start()
	{
		isActivated = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (isActivated)
		{
			if (!hasTriggeredEffects)
			{
				hasTriggeredEffects = true;
				ActivateFloatingObjects();
				ActivateParticleEffects();
				transform.GetChild(0).GetComponent<DialogueTrigger>().TriggerDialogue();
				if (player)
					player.GetComponent<OrbAbsorb>().OrbCount = 0;
			}
		}

		if (!player)
			return;


        if (player.GetComponent<OrbAbsorb>().IsOrbCounterFull() && !isActivated)
        {
            if (InputManager.Instance.GetKey(ControllerInput.ActivateAltar))
            {
                isActivated = true;
            }
        }
        else if (!player.GetComponent<OrbAbsorb>().IsOrbCounterFull() && !isActivated)
        {
            if (InputManager.Instance.GetKey(ControllerInput.ActivateAltar))
            {
                transform.GetChild(1).GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }




	}

	void ActivateFloatingObjects()
	{
		foreach (FloatingObject obj in FloatingObjects)
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
