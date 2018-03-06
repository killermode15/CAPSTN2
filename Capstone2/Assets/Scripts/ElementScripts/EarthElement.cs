using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earth Element", menuName = "Element/New Earth Element")]
public class EarthElement : Element
{
	public override bool Use(GameObject player)
	{
		if (!base.Use(player))
			return false;

		if (IsBaseUseable())
		{
			//TEMPORARY
			//Destroy(Instantiate(EarthShield, player), ShieldDuration);
			player.GetComponent<PlayerController>().anim.SetTriggerAnimParam("CastEarth");
		}
		return true;
	}



}
