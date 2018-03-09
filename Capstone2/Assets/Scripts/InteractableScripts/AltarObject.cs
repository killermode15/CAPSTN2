using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarObject : MonoBehaviour
{
	public float currentThreshold;
	public bool isActivated;
	public GameObject VFX;
	public GameObject VFXForFull;

	public bool IsFull
	{
		get { return currentAmount >= currentThreshold;  }
	}

	private GameObject player;
	private float currentAmount;
	private bool isvfxFullActive;

	// Use this for initialization
	void Start()
	{
		isActivated = false;
	}

	// Update is called once per frame
	void Update()
	{
		if(IsFull && !isvfxFullActive)
		{
			isvfxFullActive = true;
			VFXForFull.SetActive(true);
			VFXForFull.GetComponent<ParticleFollowPath>().Activate();
		}

		if (InputManager.Instance.GetKey(ControllerInput.ActivateAltar))
		{
			if (player && player.GetComponent<OrbAbsorb>().MaxOrbs == player.GetComponent<OrbAbsorb>().OrbCount)
            {
                transform.GetChild(0).GetComponent<DialogueTrigger>().TriggerDialogue();
                isActivated = true;
				player.GetComponent<OrbAbsorb>().OrbCount -= 10;
				ParticleSystem ps = VFX.GetComponent<ParticleSystem>();
			}
		}
	}

	public void ResetThreshold()
	{
		currentAmount = 0;
		isvfxFullActive = false;
		VFXForFull.SetActive(false);
		VFXForFull.GetComponent<ParticleFollowPath>().StopParticleFollow();
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
		if(other.CompareTag("Player"))
		{
			player = null;
		}
	}
}
