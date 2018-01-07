using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Element", menuName = "Element/New Wind Element")]
public class WindElement : Element {
	

	public override void Use()
	{
		base.Use();
		if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
			return;
		//TEMPORARY
		player.GetComponent<Energy>().RemoveEnergy(EnergyCost);

		//TEMPORARY
		player.position += new Vector3(0, 1.0f, 0);
	}
}
