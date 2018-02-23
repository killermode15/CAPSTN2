using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earth Element", menuName = "Element/New Earth Element")]
public class EarthElement : Element
{
	public override void Use()
	{
		base.Use();
		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;
		if (IsBaseUseable())
		{
			//TEMPORARY
			RemoveEnergy(EnergyCost);
			//Debug.Log("Not yet implemented");

			//TEMPORARY
			//Destroy(Instantiate(EarthShield, player), ShieldDuration);
			player.GetComponent<PlayerController>().anim.SetTriggerAnimParam("CastEarth");
		}
	}



}
