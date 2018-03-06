using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : State
{

	public GameObject ObjectCounterpart;

	public override void OnEnable()
	{
		base.OnEnable();
	}

	public override bool OnUpdate()
	{
		Instantiate(ObjectCounterpart, transform.position, transform.rotation);
        if (!Manager.Player.GetComponent<PlayerController>().CanMove)
			Manager.Player.GetComponent<PlayerController>().CanMove = true;
		Destroy(gameObject);
		return true;
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}
