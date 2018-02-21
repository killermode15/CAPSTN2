using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbsorbableCorruption : Absorbable
{

	public List<VFXPlayer> VFX;
	public List<PathVFX> PathVFX;

	public float ButtonTimer;

	public List<Image> ButtonImages;
	public List<Sprite> Buttons;

	private bool buttonsHasStarted;
	private int currentButtonIndex;

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
		//Play Selection VFX here
		//VFXFromString("selection").Play();
		//VFXFromString("aura ring selection").Play();
		UpdateVFX();
		if (!Input.GetButton("LeftTrigger"))
		{
			ToggleButton(false);
			IsBeingAbsorbed = false;
			IsSelected = false;
			buttonsHasStarted = false;
			StopCoroutine(ChangeButton());
		}
		else if (IsBeingAbsorbed && IsSelected)
		{
			if (!buttonsHasStarted)
			{
				ToggleButton(true);
				StartCoroutine(ChangeButton());
				buttonsHasStarted = true;
			}

			if (!HasEnergyLeft())
			{
				IsBeingAbsorbed = false;
				IsSelected = false;
			}

			string currButtonName = (currentButtonIndex < ButtonImages.Count) ? ButtonImages[currentButtonIndex].sprite.name : " ";
			if (currButtonName != " ")
			{
				if (Input.GetButtonDown(currButtonName))
				{
					if (currentButtonIndex < ButtonImages.Count)
					{
						currentButtonIndex++;
					}
					Image buttonSprite = ButtonImages[currentButtonIndex - 1];
					ButtonImages[currentButtonIndex - 1].color = new Color(buttonSprite.color.r, buttonSprite.color.g, buttonSprite.color.b, 0.4f);
				}
			}
			else
			{
				PathVFX.Find(x => x.VFXName == "vfx_CorruptionAbsorb").Play();
				GameObject.FindGameObjectWithTag("Player").GetComponent<CorruptionBar>().CurrentCorruption += (AbsorptionRate);
				Energy -= AbsorptionRate;
				currentButtonIndex = 0;
				StopAllCoroutines();
				StartCoroutine(ChangeButton());
				return;
			}
		}
		else// if(!IsSelected && IsBeingAbsorbed)
		{
			ToggleButton(false);
			IsBeingAbsorbed = false;
			buttonsHasStarted = false;
			StopCoroutine(ChangeButton());
		}
	}

	void UpdateVFX()
	{
		VFXPlayer selectionVFX = VFX.Find(x => x.VFXName == "Selection_vfx");
		if (IsSelected && selectionVFX != null)
		{
			if (!selectionVFX.isPlaying)
				selectionVFX.Play();
		}
		else if (!IsSelected && selectionVFX != null)
		{
			if (selectionVFX.isPlaying)
				selectionVFX.Stop();
		}
	}

	IEnumerator ChangeButton()
	{
		ToggleButton(true);
		currentButtonIndex = 0;
		foreach (Image button in ButtonImages)
		{
			button.sprite = Buttons[Random.Range(0, Buttons.Count)];
		}
		yield return new WaitForSeconds(ButtonTimer);
		if (CanBeAbsorbed() && IsBeingAbsorbed)
			StartCoroutine(ChangeButton());
	}

	void ToggleButton(bool val)
	{
		if (val)
		{
			foreach (Image button in ButtonImages)
			{
				button.color = new Color(button.color.r, button.color.g, button.color.b, 1);
			}
		}
		else
		{
			foreach (Image button in ButtonImages)
			{
				button.color = new Color(button.color.r, button.color.g, button.color.b, 0);
			}
		}
	}

	Sprite GetButton(string buttonName)
	{
		if (Buttons.Exists(x => x.name == buttonName))
		{
			return Buttons.Find(x => x.name == buttonName);
		}
		Debug.Log("No such button found");
		return null;
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
				//Energy -= AbsorbRate;
				IsBeingAbsorbed = true;
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
			//Debug.Break();
			throw new System.NullReferenceException("VFX [" + vfxName + "] does not exist");
		}
		return vfx;
	}

}
