using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Water Element", menuName = "Element/New Wind Element")]
public class WindElement : Element {

	public float JumpIncrease;
	public GameObject WindPush;

	public override void Use()
	{
		base.Use();
		//if (!IsElementUnlocked || IsOnCooldown || player.GetComponent<Energy>().CurrentEnergy < EnergyCost)
		//	return;

		if(IsBaseUseable())
		{
			//TEMPORARY
			RemoveEnergy(EnergyCost);

			//TEMPORARY
			//player.position += new Vector3(0, 1.0f, 0);
			player.GetComponent<PlayerController>().AddJumpVelocity(JumpIncrease);
			player.GetComponent<PlayerController>().anim.SetTriggerAnimParam("DoubleJump");

			///For the Wind Push L1
			//Debug.Log(player.transform.eulerAngles);
		}
	}
}
