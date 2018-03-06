using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Element", menuName = "Element/New Water Element")]
public class WaterElement : Element
{
	public float HealValue;
	public GameObject VFX;

	public override bool Use(GameObject player)
	{
		if (!base.Use(player))
			return false;

		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;
		if (IsBaseUseable())
		{
			//TEMPORARY
			player.GetComponent<PlayerController>().anim.SetTriggerAnimParam("CastWater");
		}
		return true;
	}
}
