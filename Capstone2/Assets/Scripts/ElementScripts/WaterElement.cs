using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Element", menuName = "Element/New Water Element")]
public class WaterElement : Element
{
	public float HealValue;
	public GameObject VFX;

	public override void Use()
	{
		base.Use();
		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;
		if (IsBaseUseable())
		{
			//TEMPORARY
			RemoveEnergy(EnergyCost);

			//TEMPORARY
			player.GetComponent<PlayerController>().anim.SetTriggerAnimParam("CastWater");
		}
	}
}
