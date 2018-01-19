using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour
{

	public string PathName;
	public float TimeToFinish;

	// Use this for initialization
	public void Activate()
	{
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "easetype", iTween.EaseType.easeInOutSine, "time", TimeToFinish));
	}
}
