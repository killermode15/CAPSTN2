using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earth Element", menuName = "Element/New Earth Element")]
public class EarthElement : Element
{
	public GameObject TerrainPrefab;
	[Tooltip("Duration of the terrain")]
	public float TerrainDuration;
	[Tooltip("Spawn offset of the terrain relative to the player")]
	public Vector3 SpawnOffset;
	[Tooltip("The height that the terrain will rise up")]
	public float TerrainHeight;

	public override bool Use(GameObject player)
	{
		if (!base.Use(player) && !player.GetComponent<PlayerController>().IsGrounded())
			return false;

		if (IsBaseUseable())
		{
			//TEMPORARY
			//Destroy(Instantiate(EarthShield, player), ShieldDuration);
			player.GetComponent<PlayerController>().anim.SetTriggerAnimParam("CastEarth");
		}
		return true;
	}



}
