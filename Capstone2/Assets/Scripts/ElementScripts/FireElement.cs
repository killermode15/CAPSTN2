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
		if (IsBaseUseable())
		{
			//TEMPORARY
			RemoveEnergy(EnergyCost);
			//Debug.Log("Not yet implemented");

			//TEMPORARY
			if (player.transform.eulerAngles.y >= 0 && player.transform.eulerAngles.y <= 180) {
				Sphere.GetComponent<Mover> ().isRight = true;
			} else { //if (player.transform.eulerAngles.y <= 0)
				Sphere.GetComponent<Mover> ().isRight = false;
			}
			GameObject ball = Instantiate(Sphere, player.position, Quaternion.identity);
			ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
	}
}
