using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour {

	public float CurrentEnergy;
	public float MaxEnergy;
	public Slider EnergyBar;

	private float energyPercent;
	private float dampTime = 5f;
	private float currentLerpTime;

	// Use this for initialization
	void Start () {
		CurrentEnergy = 0;
		if (!EnergyBar)
		{
			EnergyBar = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<Slider>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		energyPercent = CurrentEnergy / MaxEnergy;
		if (EnergyBar.value != energyPercent)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > dampTime)
			{
				currentLerpTime = dampTime;
			}

			EnergyBar.value = Mathf.Lerp(EnergyBar.value, energyPercent, currentLerpTime / dampTime);
		}
		else
			currentLerpTime = 0;
	}

	public void AddEnergy(float val)
	{
		CurrentEnergy += val;
		if(CurrentEnergy > MaxEnergy)
		{
			CurrentEnergy = MaxEnergy;
		}
		currentLerpTime = 0;
	}

	public void RemoveEnergy(float val)
	{
		CurrentEnergy -= val;
		if (CurrentEnergy < 0)
			CurrentEnergy = 0;
		currentLerpTime = 0;
	}
}
