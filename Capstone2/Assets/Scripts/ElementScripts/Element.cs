using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
	Earth,
	Water,
	Wind,
	Light
}

//[CreateAssetMenu(fileName = "New Element", menuName = "Element/New Element")]
public class Element : ScriptableObject {

	public ElementType Type;
	public Sprite ElementIcon;
	public float CooldownDuration;
	public bool IsElementUnlocked;
	public bool IsOnCooldown;
	

	public virtual bool Use(GameObject player)
	{
		if (!player)
			return false;
		player.GetComponent<PlayerController>().anim.SetBoolAnimParam("CastingElement", true);
		return true;
	}

	public bool IsBaseUseable()
	{
		return !IsOnCooldown;
	}
}
