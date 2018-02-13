using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
	Earth,
	Water,
	Wind,
	Fire
}

//[CreateAssetMenu(fileName = "New Element", menuName = "Element/New Element")]
public class Element : ScriptableObject {

	public ElementType Type;
	public Sprite ElementIcon;
	public Color EnergyColor;
	public float EnergyCost;
	public float CurrentUseableEnergy;
	public float CooldownDuration;
	public bool IsElementUnlocked;
	public bool IsOnCooldown;
	public float MaxUseableEnergy;
	
	protected Transform player;

	

	public void AddEnergy(float val)
	{
		CurrentUseableEnergy += val;
		if (CurrentUseableEnergy > MaxUseableEnergy)
		{
			CurrentUseableEnergy = MaxUseableEnergy;
		}
	}

	public void RemoveEnergy(float val)
	{
		CurrentUseableEnergy -= val;
		if (CurrentUseableEnergy < 0)
			CurrentUseableEnergy = 0;
	}

	public virtual void Use()
	{
		if (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
		player.GetComponent<PlayerController>().anim.SetBoolAnimParam("CastingElement", true);


		//throw new System.NullReferenceException("This is the base class");
	}

	public virtual void SecondaryUse()
	{
		if (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	public bool IsBaseUseable()
	{
		return CurrentUseableEnergy >= EnergyCost && !IsOnCooldown;
	}
}
