using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarObject : MonoBehaviour
{

	public GameObject VFX;
	private GameObject player;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (InputManager.Instance.GetKey(ControllerInput.ActivateAltar))
		{
			if (player)
			{
				CorruptionBar corruption = player.GetComponent<CorruptionBar>();
				ParticleSystem ps = VFX.GetComponent<ParticleSystem>();
				corruption.ReleaseCorruption();
				if (corruption.CurrentCorruption > 0)
				{
					if (!ps.isPlaying)
						ps.Play();
				}
				//else
				//{
				//	if (ps.isPlaying)
				//		ps.Stop();
				//}
			}
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
		if(other.CompareTag("Player"))
		{
			player = null;
		}
	}
}
