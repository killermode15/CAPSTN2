using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fire Element", menuName = "Element/New Fire Element")]
public class FireElement : Element
{

	public GameObject Sphere;

	public override void Use()
	{
		base.Use();
		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;
		//TEMPORARY
		player.GetComponent<Energy>().RemoveEnergy(EnergyCost);
		//Debug.Log("Not yet implemented");

		//TEMPORARY
		GameObject ball = Instantiate(Sphere, player.position, Quaternion.identity);
		ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		ball.AddComponent<Mover>();

	}
}
