using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbableObject : Absorbable
{
	public ElementType Type;

	public List<VFXPlayer> VFX;
	public List<PathVFX> PathVFX;

	// Use this for initialization
	public override void Start()
	{
		foreach (VFXPlayer vfx in VFX)
		{
			vfx.Stop();
		}
		foreach (PathVFX vfx in PathVFX)
		{
			vfx.Stop();
		}
		base.Start();
	}

	// Update is called once per frame
	public override void Update()
	{
		base.Update();
		if (IsSelected)
		{
			//Play Selection VFX here
			//VFXFromString("selection").Play();
			//VFXFromString("aura ring selection").Play();
		}
		else
		{
			//VFXFromString("selection").Stop();
			//VFXFromString("aura ring selection").Stop();
		}
	}

	public override void InteractWith()
	{
		if (CanBeAbsorbed())
		{
			if (IsAbsorbing())
			{
				//VFX.Find(x => x.VFXName.ToLower() == "absorb").Play();
				//Play Absorb VFX here
				//Absorb enemy
				Energy -= AbsorbRate;
			}
		}
		else if (!HasEnergyLeft())
		{
			Debug.Log("\"I have no more energy\" said " + gameObject.name + " the object");
			IsSelected = false;
		}

	}


	public VFXPlayer VFXFromString(string vfxName)
	{
		VFXPlayer vfx = (VFX.Find(x => x.VFXName.ToLower() == vfxName)) == null ?
			PathVFX.Find(x => x.VFXName.ToLower() == vfxName) : (VFX.Find(x => x.VFXName.ToLower() == vfxName));

		return vfx;
	}
}
