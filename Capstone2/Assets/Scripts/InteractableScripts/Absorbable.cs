using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyType
{
	Element,
	Corruption
}

public class Absorbable : MonoBehaviour, IInteractable {

	public EnergyType EnergyType;
	public float Energy;
	public float AbsorptionRate;
	public bool IsSelected;
	public bool IsBeingAbsorbed;

	public float AbsorbRate
	{
		get
		{
			return AbsorptionRate * Time.deltaTime;
		}
	}

	protected float maxEnergy;
	protected GameObject player;

	// Use this for initialization
	public virtual void Start () {
		maxEnergy = Energy;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(!CanBeAbsorbed())
		{
			IsBeingAbsorbed = false;
			IsSelected = false;
		}
	}

	public virtual void InteractWith()
	{
		throw new NotImplementedException("This is the base class");
	}

	public bool HasEnergyLeft()
	{
		return Energy >= 0;
	}

	public bool CanBeAbsorbed()
	{
		return HasEnergyLeft() && IsSelected;
	}

	public bool IsAbsorbing()
	{
		return InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy);
	}
}
