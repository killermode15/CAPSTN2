using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathVFX : VFXPlayer
{

	public bool Loop;
	

	public override void Play()
	{
		if (!isPlaying)
		{
			VFX.GetComponent<ParticleFollowPath>().Activate();
			isPlaying = true;
			isPaused = false;
		}
	}

	public override void Pause()
	{

	}

	public override void Stop()
	{
		if (isPlaying || isPaused)
		{
			VFX.GetComponent<ParticleFollowPath>().StopParticleFollow();
			isPlaying = false;
			isPaused = false;
		}
	}
}
