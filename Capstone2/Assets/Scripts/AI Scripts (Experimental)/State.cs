using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour {
	[HideInInspector] public StateManager Manager;
	// Use this for initialization
	public virtual void OnEnable()
	{
		if(!Manager)
		{
			Manager = GetComponent<StateManager>();
		}
	}

	public virtual bool OnUpdate()
	{
		return false;
	}

	public virtual void OnDisable()
	{

	}
}
