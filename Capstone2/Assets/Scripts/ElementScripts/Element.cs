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
	public float EnergyCost;
	public float ModifierEnergyCost;
	public float CooldownDuration;
	public bool IsElementUnlocked;
	public bool IsOnCooldown;
	public bool IsModifier;
	public string MoveNameToModify;
	
	protected Transform player;

	public virtual void Use()
	{
		if (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}


		//throw new System.NullReferenceException("This is the base class");
	}

	public virtual void ModifyMove()
	{
		if (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
}
