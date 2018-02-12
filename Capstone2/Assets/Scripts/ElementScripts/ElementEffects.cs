using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffects : MonoBehaviour
{

	[Header("Earth Element Variables")]
	public GameObject TerrainPrefab;
	public float SpawnDistance;

	public void SummonTerrain()
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
}
