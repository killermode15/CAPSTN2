using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {

	// Use this for initialization
	public virtual void OnEnable()
	{
		
	}

	public virtual bool OnUpdate()
	{
		return false;
	}

	public virtual void OnDisable()
	{

	}
}
