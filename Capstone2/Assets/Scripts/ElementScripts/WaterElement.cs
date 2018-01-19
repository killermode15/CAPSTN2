using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Element", menuName = "Element/New Water Element")]
public class WaterElement : Element {

	public GameObject VFX;

	public override void Use()
	{
		base.Use();
		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;
		//TEMPORARY
		player.GetComponent<Energy>().RemoveEnergy(EnergyCost);

		//TEMPORARY
		player.GetComponent<HP>().AddHealth(100);
		GameObject spawnedVFX = Instantiate(VFX, player.transform.position, Quaternion.identity);
		spawnedVFX.GetComponent<ParticleFollowPath>().Activate();
		Destroy(spawnedVFX, spawnedVFX.GetComponent<ParticleFollowPath>().TimeToFinish + 0.5f);
	}
}
