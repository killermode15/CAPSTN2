using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VFXPlayer
{

	public string VFXName;
	public GameObject VFX;

	[HideInInspector] public bool isPlaying;
	[HideInInspector] public bool isPaused;

	public virtual void Play()
	{
		if (!isPlaying)
		{
			VFX.GetComponent<ParticleSystem>().Play();
			isPlaying = true;
			isPaused = false;
		}

	}

	public virtual void Stop()
	{
		if (isPlaying || isPaused)
		{
			VFX.GetComponent<ParticleSystem>().Stop();
			isPlaying = false;
			isPaused = false;
		}
	}

	public virtual void Pause()
	{
		if (!isPaused)
		{
			VFX.GetComponent<ParticleSystem>().Pause();
			isPaused = true;
			isPlaying = false;
		}
	}
}
