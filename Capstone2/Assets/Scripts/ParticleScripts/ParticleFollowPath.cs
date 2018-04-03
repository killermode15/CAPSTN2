using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour
{

	public string PathName;
	public float TimeToFinish;
	public bool Loop;
	public iTween.EaseType EaseType = iTween.EaseType.easeInOutSine;
	//public iTween.LoopType LoopType = iTween.LoopType.none;

	// Use this for initialization
	public void Activate()
	{
		Hashtable hash = new Hashtable
		{
			{ "path", iTweenPath.GetPath(PathName) },
			{ "easetype", EaseType },
			{ "time", TimeToFinish }
		};
		iTween.MoveTo(gameObject, hash);

		if (Loop)
		{
			StartCoroutine(StartLoop());
		}
	}

	public void StopParticleFollow()
	{
		iTween.StopByName(PathName);
		StopCoroutine(StartLoop());
	}

	IEnumerator StartLoop()
	{
		yield return new WaitForSeconds(TimeToFinish - (TimeToFinish * 0.2f));
		iTween.StopByName(PathName);
		Activate();
	}
}
