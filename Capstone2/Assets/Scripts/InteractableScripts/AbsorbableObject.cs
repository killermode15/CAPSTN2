using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbableObject : MonoBehaviour, IInteractable {

	//The amount of energy the object has
	public float Energy;
	//Reference to the meter
	public Image AbsorbMeter;
	//If the object is currently selected for absorption
	public bool IsSelected;
	//If the object is currently being absorbed
	public bool IsAbsorbing;

	public float AbsorbedEnergy
	{
		get
		{
			return absorbedEnergy;
		}
		set
		{
			absorbedEnergy = value;
		}
	}

	//Reference to the canvas
	private GameObject myCanvas;
	//Max amount of energy the object has
	private float maxEnergy;
	private float absorptionRate;
	private float absorbedEnergy;

	// Use this for initialization
	void Start ()
	{
		maxEnergy = Energy;
		myCanvas = transform.Find("Canvas").gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		if(!IsSelected || !IsAbsorbing)
		{
			if (absorptionRate > 0)
				absorptionRate = 0;
		}

		if(IsSelected)
		{
			myCanvas.SetActive(true);
		}
		else
		{
			myCanvas.SetActive(false);
		}
	}

	public bool HasEnergyLeft()
	{
		if(Energy <= 0)
		{
			return false;
		}
		return true;
	}

	public float AbsorbObject()
	{
		if(IsSelected)
		{
			absorptionRate += Time.deltaTime;
			Energy -= absorptionRate;
			float energyPercent = Energy / maxEnergy;
			AbsorbMeter.fillAmount = energyPercent;

			if (!HasEnergyLeft())
				Destroy(gameObject);

			return absorptionRate;
		}
		return 0;
	}

	public void InteractWith()
	{
		AbsorbedEnergy = AbsorbObject();
	}
}
