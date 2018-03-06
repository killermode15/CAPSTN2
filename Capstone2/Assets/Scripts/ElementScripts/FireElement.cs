using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fire Element", menuName = "Element/New Fire Element")]
public class FireElement : Element
{

	public GameObject Sphere;
	public GameObject StunPrefab;

	public override bool Use(GameObject player)
	{
		if (!base.Use(player))
			return false;

		if (IsBaseUseable())
		{

			///STUN TEST
			if (player.transform.eulerAngles.y >= 0 && player.transform.eulerAngles.y <= 180) 
				StunPrefab.GetComponent<Mover>().isRight = true;
			else 
				StunPrefab.GetComponent<Mover>().isRight = false;

			GameObject Push = Instantiate(StunPrefab, player.transform.position, Quaternion.identity);
		}
		return true;
	}
}
