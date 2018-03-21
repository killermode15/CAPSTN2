using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlantCollider : MonoBehaviour
{

	public List<Collider> PlantColliders;
	public bool DisableOnStart = true;


	private void Start()
	{
		if (DisableOnStart)
		{
			foreach (Collider col in PlantColliders)
			{
				col.enabled = false;
			}
		}
	}

	// Update is called once per frame
	public void ActivateColliders()
	{
		foreach (Collider col in PlantColliders)
		{
			col.enabled = true;
		}
	}
}
