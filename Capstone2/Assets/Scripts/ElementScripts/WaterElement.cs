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
			//player.GetComponent<PlayerController>().anim.SetTriggerAnimParam("CastWater");
			GameObject waterVFX = Instantiate(VFX, player.transform);
			waterVFX.transform.localPosition = Vector3.zero;
			Destroy(waterVFX, waterVFX.GetComponent<ParticleSystem>().main.duration);

			List<Collider> detectedPlants = Physics.OverlapSphere(player.transform.position, WaterRange).ToList();
			foreach (Collider plant in detectedPlants)
			{
				if (plant.GetComponent<Plant>())
					plant.GetComponent<Plant>().ActivatePlant();
			}
		}
		return true;
	}
}
