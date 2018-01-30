using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earth Element", menuName = "Element/New Earth Element")]
public class EarthElement : Element {

	public float ShieldDuration;
	public GameObject EarthShield;

	public override void Use()
	{
		base.Use();
		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;
		//TEMPORARY
		player.GetComponent<Energy>().RemoveEnergy(EnergyCost);
		//Debug.Log("Not yet implemented");

		//TEMPORARY
		Destroy(Instantiate(EarthShield, player), ShieldDuration);
	}

	public override void ModifyMove()
	{
		base.Use();

		player.GetComponent<UseSkill>().SetElementOnCooldown(this);

		player.GetComponent<Energy>().RemoveEnergy(ModifierEnergyCost);
		GameObject shield = Instantiate(EarthShield, player);
		//shield.transform.parent = player.transform;

		//shield.transform.localPosition += new Vector3(0, 0, 0.55f);

		Destroy(shield, ShieldDuration);
	}
}
