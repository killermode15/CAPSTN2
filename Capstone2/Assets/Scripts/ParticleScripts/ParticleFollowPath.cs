using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour
{

	public string PathName;
	public float TimeToFinish;
	public iTween.EaseType EaseType = iTween.EaseType.easeInOutSine;
	public iTween.LoopType LoopType = iTween.LoopType.none;

	// Use this for initialization
	public void Activate()
	{
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "easetype", EaseType, "time", TimeToFinish));//, "looptype", LoopType));
	}

	public void StopParticleFollow()
	{
		iTween.StopByName(PathName);
	}
}
