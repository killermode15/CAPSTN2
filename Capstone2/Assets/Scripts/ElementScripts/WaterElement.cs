using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Element", menuName = "Element/New Water Element")]
public class WaterElement : Element {
	

	public override void Use()
	{
		base.Use();
		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;
		//TEMPORARY
		player.GetComponent<Energy>().RemoveEnergy(EnergyCost);

		//TEMPORARY
		player.GetComponent<HP>().AddHealth(100);
	}
}
