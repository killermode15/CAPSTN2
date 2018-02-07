using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbableObject : Absorbable
{
	public ElementType Type;
	public VFXPlayer playingvfx;
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
			IsBeingAbsorbed = false;
			//VFXFromString("selection").Stop();
			//VFXFromString("aura ring selection").Stop();
		}
	}

	public override void InteractWith()
	{
		if (CanBeAbsorbed())
		{
			if (IsBeingAbsorbed)
			{

				//Play Absorb VFX here
				//Absorb enemy
				VFXFromString("AbsorbVFX").Play();
				IsBeingAbsorbed = true;
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
		VFXPlayer vfx = null;

		//= (VFX.Find(x => x.VFXName.ToLower() == vfxName)) == null ?
		//	(VFX.Find(x => x.VFXName.ToLower() == vfxName)) : (VFX.Find(x => x.VFXName.ToLower() == vfxName));

		if (VFX.Exists(x => x.VFXName ==vfxName))
		{
			vfx = VFX.Find(x => x.VFXName == vfxName);
		}
		else if(PathVFX.Exists(x => x.VFXName == vfxName))
		{
			vfx = PathVFX.Find(x => x.VFXName == vfxName);
		}

		if (vfx == null)
		{
			Debug.Break();
			throw new System.NullReferenceException("VFX [" + vfxName + "] does not exist");
		}
		return vfx;
	}
}
