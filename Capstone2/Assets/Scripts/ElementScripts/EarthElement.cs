using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earth Element", menuName = "Element/New Earth Element")]
public class EarthElement : Element {

	public GameObject Sphere;

	public override void Use()
	{
		base.Use();
		if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
			return;
		//TEMPORARY
		player.GetComponent<Energy>().RemoveEnergy(EnergyCost);
		//Debug.Log("Not yet implemented");

		//TEMPORARY
		Destroy(Instantiate(Sphere, player.position, Quaternion.identity), 2);
	}
}
