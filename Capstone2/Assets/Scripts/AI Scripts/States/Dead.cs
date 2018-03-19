﻿using System.Collections;
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
        Debug.Log("Dead");
        GetComponentInChildren<Animator>().SetBool("Slither", false);
        GetComponentInChildren<Animator>().SetBool("Bite", false);
        Instantiate(ObjectCounterpart, new Vector3(transform.position.x, transform.position.y + 7.0f, transform.position.z), transform.rotation);
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
