using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarObject : MonoBehaviour
{
	public float MaximumThreshold;
	public bool isActivated;
	public GameObject VFX;
	public GameObject VFXForFull;

	public bool IsFull
	{
		get { return currentAmount >= MaximumThreshold;  }
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
			if (player)
			{
				isActivated = true;
				CorruptionBar corruption = player.GetComponent<CorruptionBar>();
				ParticleSystem ps = VFX.GetComponent<ParticleSystem>();
				corruption.ReleaseCorruption();
				currentAmount += Time.deltaTime * corruption.CorruptionReleaseRate;
				if (corruption.CurrentCorruption > 0)
				{
					if (!ps.isPlaying)
						ps.Play();
				}
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
