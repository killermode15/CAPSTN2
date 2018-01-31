using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbableCorruption : Absorbable {

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public override void InteractWith()
	{
		if(CanBeAbsorbed())
		{
			//Play Selection VFX
			if(IsAbsorbing())
			{
				//Absorb enemy
			}
		}
	}

}
