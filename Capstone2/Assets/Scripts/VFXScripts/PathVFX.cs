using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PathVFX : VFXPlayer
{
	public bool IsInstanced;
	public bool Loop;


	public override void Play()
	{
		if (!isPlaying)
		{
			if (IsInstanced)
			{
				GameObject newVFX = MonoBehaviour.Instantiate(VFX, VFX.transform.parent);
				//newVFX.transform.parent = (VFX.transform.parent);//, true);
				//newVFX.transform.localPosition = VFX.transform.localPosition;
				newVFX.GetComponent<ParticleFollowPath>().Activate();
				MonoBehaviour.Destroy(newVFX, newVFX.GetComponent<ParticleFollowPath>().TimeToFinish + 0.5f);
			}
			else
			{
				VFX.GetComponent<ParticleFollowPath>().Activate();
				isPlaying = true;
				isPaused = false;
			}
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
