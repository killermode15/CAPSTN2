	using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffects : MonoBehaviour
{

	 public bool isCasting;

	[Header("Earth Element Variables")]
	public GameObject TerrainPrefab;
	public int TerrainHeight;
	public float BlockOffset;
	public float SpawnDistance;

	private List<GameObject> spawnedTerrain;

	[Space]
	[Header("Water Element Variables")]
	public GameObject WaterVFX;

	[Space]
	[Header("Light Element Variables")]
	public GameObject StunPrefab;

	void Update(){

	}

	#region Earth Element Implementation
	public void SummonTerrain(EarthElement earthElement)
	{
		GameObject terrainSpawned = Instantiate(earthElement.TerrainPrefab, 
									transform.parent.position +  earthElement.SpawnOffset, 
									Quaternion.identity);

		StartCoroutine(RaiseTerrain(terrainSpawned, earthElement));

		#region Old Implementation of Earth Element
		//Vector3 location;
		/////Terrain Effect
		//if (transform.eulerAngles.y >= 0 && transform.eulerAngles.y <= 180)
		//{
		//	//right
		//	location = new Vector3(transform.position.x + SpawnDistance, transform.position.y, transform.position.z);
		//}
		//else
		//{
		//	location = new Vector3(transform.position.x - SpawnDistance, transform.position.y, transform.position.z);
		//}
		//StartCoroutine(SpawnTerrainChunks(location));
		//GameObject Terrain = Instantiate(TerrainPrefab, location, Quaternion.identity);
		//StartCoroutine(DestroyObject(Terrain, 5.0f));
		#endregion
	}

	IEnumerator RaiseTerrain(GameObject terrain, EarthElement earthElement)
	{
		Vector3 originalPosition = terrain.transform.position;
		Vector3 desiredPosition = originalPosition + new Vector3(0, earthElement.TerrainHeight, 0);

		while(Vector3.Distance(desiredPosition, terrain.transform.position) > 1)
		{

			terrain.transform.position = Vector3.Lerp(terrain.transform.position, desiredPosition, Time.deltaTime);

			yield return new WaitForEndOfFrame();
		}
		StartCoroutine(DestroyObject(terrain, earthElement.TerrainDuration));
	}

	/*
	IEnumerator SpawnTerrainChunks(Vector3 location)
	{
		spawnedTerrain = new List<GameObject>();
		int index = 0;
		while(spawnedTerrain.Count < TerrainHeight)
		{
			GameObject Terrain = Instantiate(TerrainPrefab, location + new Vector3(0, BlockOffset * index), Quaternion.identity);
			index++;
			StartCoroutine(DestroyObject(Terrain, 5.0f));
			spawnedTerrain.Add(Terrain);
			yield return new WaitForSeconds(0.15f);
		}
	}
	*/

	#endregion

	#region Helper Functions

	IEnumerator DestroyObject(GameObject obj, float delay)
	{
		float timer = delay;

		while (timer > 0) {

			if (!PauseManager.Instance.IsPaused) {
				timer -= Time.deltaTime;
			}

			yield return new WaitForEndOfFrame ();
		}
		Destroy (obj);
	}

	#endregion

	public void CastHeal(WaterElement waterElement)
	{
		//GetComponentInParent<HP>().AddHealth(waterElement.HealValue);
		GameObject spawnedVFX = Instantiate(waterElement.VFX, transform.parent.transform.position, Quaternion.identity);
		spawnedVFX.GetComponent<ParticleFollowPath>().Activate();
		Destroy(spawnedVFX, spawnedVFX.GetComponent<ParticleFollowPath>().TimeToFinish + 0.5f);
	}

	public void StopCast()
	{
		isCasting = false;
		GetComponentInParent<PlayerController>().anim.SetBoolAnimParam("CastingElement", false);
	}

	public void Stun()
	{
		/*if (player.transform.eulerAngles.y >= 0 && player.transform.eulerAngles.y <= 180)
		{
			WindPush.GetComponent<Mover>().isRight = true;
		}
		else
		{ //if (player.transform.eulerAngles.y <= 0)
			WindPush.GetComponent<Mover>().isRight = false;
		}
		GameObject Push = Instantiate(WindPush, player.position, Quaternion.identity);*/
		if (transform.parent.transform.eulerAngles.y >= 0 && transform.parent.transform.eulerAngles.y <= 180)
			StunPrefab.GetComponent<Mover>().isRight = true;
		else
			StunPrefab.GetComponent<Mover>().isRight = false;

		GameObject Push = Instantiate(StunPrefab, transform.parent.position, Quaternion.identity);
	}
}
