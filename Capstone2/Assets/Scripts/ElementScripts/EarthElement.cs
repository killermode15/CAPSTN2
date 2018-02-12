using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earth Element", menuName = "Element/New Earth Element")]
public class EarthElement : Element
{

	public float ShieldDuration;
	public GameObject EarthShield;
	public GameObject EarthTerrain;
	public GameObject EarthTrap;
	Vector3 location;
	public float spawnDistance;

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


			///Trap Effect
			/*
			if (player.transform.eulerAngles.y >= 0 && player.transform.eulerAngles.y <= 180) {
				//right
				location = new Vector3(player.transform.position.x + spawnDistance, player.transform.position.y, player.transform.position.z);
			} else {
				location = new Vector3(player.transform.position.x - spawnDistance, player.transform.position.y, player.transform.position.z);
			}
			GameObject Terrain = Instantiate (EarthTrap, location, Quaternion.identity);
			Destroy (Terrain, 5.0f);*/
		}
	}


	public override void SecondaryUse()
	{
		base.Use();

		if (IsBaseUseable())
		{
			RemoveEnergy(SecondaryUseEnergyCost);

			if (player.transform.eulerAngles.y >= 0 && player.transform.eulerAngles.y <= 180)
			{
				//right
				location = new Vector3(player.transform.position.x + spawnDistance, player.transform.position.y, player.transform.position.z);
			}
			else
			{
				location = new Vector3(player.transform.position.x - spawnDistance, player.transform.position.y, player.transform.position.z);
			}
			GameObject Terrain = Instantiate(EarthTrap, location, Quaternion.identity);
			Destroy(Terrain, 5.0f);
		}
		/*
		if (IsModifierUseable())
		{
			player.GetComponent<UseSkill>().SetElementOnCooldown(this);

			RemoveEnergy(ModifierEnergyCost);
			GameObject shield = Instantiate(EarthShield, player);
			//shield.transform.parent = player.transform;

			//shield.transform.localPosition += new Vector3(0, 0, 0.55f);

			Destroy(shield, ShieldDuration);
		}*/
	}
}
