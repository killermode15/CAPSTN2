using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffects : MonoBehaviour
{

	[Header("Earth Element Variables")]
	public GameObject TerrainPrefab;
	public float SpawnDistance;

	[Space]
	[Header("Water Element Variables")]
	public GameObject WaterVFX;


	public void SummonTerrain(EarthElement earthElement)
	{
		Vector3 location;
		///Terrain Effect
		if (transform.eulerAngles.y >= 0 && transform.eulerAngles.y <= 180)
		{
			//right
			location = new Vector3(transform.position.x + SpawnDistance, transform.position.y, transform.position.z);
		}
		else
		{
			location = new Vector3(transform.position.x - SpawnDistance, transform.position.y, transform.position.z);
		}
		GameObject Terrain = Instantiate(TerrainPrefab, location, Quaternion.identity);
		Destroy(Terrain, 5.0f);
	}

	public void CastHeal(WaterElement waterElement)
	{
		GetComponentInParent<HP>().AddHealth(waterElement.HealValue);
		GameObject spawnedVFX = Instantiate(waterElement.VFX, transform.parent.transform.position, Quaternion.identity);
		spawnedVFX.GetComponent<ParticleFollowPath>().Activate();
		Destroy(spawnedVFX, spawnedVFX.GetComponent<ParticleFollowPath>().TimeToFinish + 0.5f);
	}

	public void StopCast()
	{
		GetComponentInParent<PlayerController>().anim.SetBoolAnimParam("CastingElement", false);
	}
}
