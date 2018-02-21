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
		if (!Input.GetButton("LeftTrigger"))
		{
			//IsBeingAbsorbed = false;
			//player.SendMessage("SetCanMove", true);
			//PlayerAnimation anim = player.GetComponent<PlayerAnimation>();
			//if (anim.GetBoolAnimParam("IsAbsorbing"))
			//{
			//	anim.SetBoolAnimParam("IsAbsorbing", false);
			//}
		}
		if (IsSelected)
		{
			//PlayerAnimation anim = player.GetComponent<PlayerAnimation>();
			//if (!anim.GetBoolAnimParam("IsAbsorbing"))
			//{
			//	anim.SetBoolAnimParam("IsAbsorbing", true);
			//}
			//Play Selection VFX here
			VFXFromString("Selection_vfx").Play();
			//VFXFromString("aura ring selection").Play();
		}
		else if (!IsSelected)
		{
			IsBeingAbsorbed = false;
			VFXFromString("Selection_vfx").Stop();
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
				VFXFromString("Absorb_vfx").Play();
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

		if (VFX.Exists(x => x.VFXName == vfxName))
		{
			vfx = VFX.Find(x => x.VFXName == vfxName);
		}
		else if (PathVFX.Exists(x => x.VFXName == vfxName))
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
