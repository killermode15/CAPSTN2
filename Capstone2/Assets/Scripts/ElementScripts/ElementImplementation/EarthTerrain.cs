using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTerrain : MonoBehaviour {

	public int TerrainHeight;

	public bool IsDoneSpawning()
	{
		if (TerrainHeight <= 0)
			return false;
		else
			return true;
	}

}
