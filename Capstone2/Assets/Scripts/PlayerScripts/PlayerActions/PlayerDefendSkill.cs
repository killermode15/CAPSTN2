using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefendSkill : MonoBehaviour, IPlayerAction
{
	[Tooltip("The percentage of the max capacity of the shield required before being useable again")]
	[Range(0,1)]
	public float MaximumCapacityBeforeUseable;
	public float ShieldRegenerationRate;
	public float Capacity;
	public GameObject Shield;
	public Element ElementModifier;

	private float currentCooldown;
	private float defenseTimer;
	private float maxCapacity;
	private bool isDefendActive;
	private bool isUseable;

	// Use this for initialization
	void Start()
	{
		maxCapacity = Capacity;
		Shield.GetComponent<ParticleSystem>().Stop();
		Shield.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		UseAction();
		if(Input.GetKeyDown(KeyCode.P))
		{
			Capacity -= 10;
		}

		if(!isDefendActive)
		{
			if(Capacity < maxCapacity)
			{
				Capacity += ShieldRegenerationRate * Time.deltaTime;
			}
		}

		float percRequirement = maxCapacity * MaximumCapacityBeforeUseable;
		if(Capacity <= 0)
		{
			isUseable = false;
		}
		else if (!isUseable && Capacity >= percRequirement)
		{
			isUseable = true;
		}

	}

	public void UseAction()
	{
		//throw new NotImplementedException();
		///Gameobject Shield
		/// NOTE: I made a Shield script (for the collision of the gameobject) "Shield.cs"
		if (InputManager.Instance.GetKey(ControllerInput.Defend) && Capacity > 0 && isUseable && GetComponent<PlayerController>().CanMove)
		{
			//Activate shield
			if (!isDefendActive)
			{
				isDefendActive = true;
				Shield.SetActive(true);
				Shield.GetComponent<ParticleSystem>().Play();
			}
		}
		else
		{
			if (isDefendActive)
			{
				isDefendActive = false;
				Shield.GetComponent<ParticleSystem>().Stop();
				Shield.SetActive(false);
			}
		}
	}
}
