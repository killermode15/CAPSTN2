using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEffects : MonoBehaviour
{

	[Header("Earth Element Variables")]
	public GameObject TerrainPrefab;
	public float SpawnDistance;

	[Space]
	[Header("Light Element Variables")]
	public GameObject StunPrefab;

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

	public void Stun(){
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
