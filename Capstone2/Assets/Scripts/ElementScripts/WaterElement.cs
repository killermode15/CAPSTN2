using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Element", menuName = "Element/New Water Element")]
public class WaterElement : Element
{
	public float WaterRange;
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

			List<Collider> detectedPlants = Physics.OverlapSphere(player.transform.position, WaterRange).ToList();
			foreach(Collider plant in detectedPlants)
			{
				plant.GetComponent<Plant>().ActivatePlant();
			}
		}
		return true;
	}
}
