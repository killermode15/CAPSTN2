using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbableObject : MonoBehaviour, IInteractable
{

	//The amount of energy the object has
	public float Energy;
	//If the object is currently selected for absorption
	public bool IsSelected;
	//If the object is currently being absorbed
	public bool IsAbsorbing;
	//If the object can still be absorbed
	public bool CanBeAbsorbed = true;
	//Reference to the selection particle system
	public GameObject SelectedVFX;
	//Reference to other VFX
	public List<GameObject> OtherVFX;
	//Reference to absorb vfx path
	public iTweenPath AbsorbVFXPath;
	//Reference to absorb vfx
	public GameObject AbsorbVFX;

	public float AbsorbedEnergy
	{
		get
		{
			return absorbedEnergy;
		}
		set
		{
			absorbedEnergy = value;
		}
	}

	////Reference to the canvas
	//private GameObject myCanvas;
	//Max amount of energy the object has
	private float maxEnergy;
	private float absorptionRate;
	private float absorbedEnergy;
	private bool isSelectionVFXPlaying;
	private bool isAbsorbVFXPlaying;
	private bool shouldAbsorbVFXPlay;
	private GameObject player;

	// Use this for initialization
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		if (!SelectedVFX)
			SelectedVFX = transform.Find("vfx_TreeSelection").gameObject;
		maxEnergy = Energy;
		ToggleOtherSelectionVFX(false);
		CanBeAbsorbed = true;
		//myCanvas = transform.Find("Canvas").gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		VFXHandler();
		if (IsSelected)
		{
			if (CanBeAbsorbed)
			{
				if (InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy))
				{
					AbsorbVFXPath.nodes[AbsorbVFXPath.nodeCount - 1] = player.transform.position;
					IsAbsorbing = true;
					shouldAbsorbVFXPlay = true;
				}
				else
				{
					shouldAbsorbVFXPlay = false;
					IsAbsorbing = false;
				}
			}
		}
		else if (!IsSelected && !InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy))
		{
			IsAbsorbing = false;
			shouldAbsorbVFXPlay = false;

			if (absorptionRate > 0)
				absorptionRate = 0;

			if (isSelectionVFXPlaying)
			{
				StopCoroutine(ActivateSelectionVFX());
				ToggleOtherSelectionVFX(false);
				isSelectionVFXPlaying = false;
			}
			if (isAbsorbVFXPlaying)
			{
				isAbsorbVFXPlaying = false;
				StopCoroutine(ActivateAbsorbVFX());
			}

		}
		#region 
		//	if (CanBeAbsorbed)
		//	{
		//		//If the player is 
		//		if (!IsSelected || !IsAbsorbing)
		//		{
		//			if (absorptionRate > 0)
		//				absorptionRate = 0;
		//		}

		//		if (IsSelected)
		//		{
		//			if (!isSelectionVFXPlaying)
		//			{
		//				StartCoroutine(ActivateSelectionVFX());
		//				ToggleOtherSelectionVFX(true);
		//			}

		//			if(IsAbsorbing)
		//			{
		//				AbsorbVFXPath.nodes[AbsorbVFXPath.nodeCount - 1] = player.transform.position;
		//				//Play VFX
		//				if(!isAbsorbVFXPlaying)
		//				{
		//					ToggleAbsorbVFX(true);
		//				}
		//			}
		//			else
		//			{
		//				if (isAbsorbVFXPlaying)
		//				{
		//					ToggleAbsorbVFX(false);
		//				}
		//			}
		//		}
		//		else if (!IsSelected)
		//		{
		//			if (isSelectionVFXPlaying)
		//			{
		//				StopCoroutine(ActivateSelectionVFX());
		//				ToggleOtherSelectionVFX(false);
		//				isSelectionVFXPlaying = false;
		//			}
		//			if (isAbsorbVFXPlaying)
		//			{
		//				isAbsorbVFXPlaying = false;
		//				StopCoroutine(ActivateAbsorbVFX());
		//			}
		//		}
		//	}

		//	else if (!IsSelected || !CanBeAbsorbed)
		//	{
		//		if (isSelectionVFXPlaying)
		//		{
		//			StopCoroutine(ActivateSelectionVFX());
		//			ToggleOtherSelectionVFX(false);
		//			isSelectionVFXPlaying = false;
		//		}
		//		if (isAbsorbVFXPlaying)
		//		{
		//			isAbsorbVFXPlaying = false;
		//			StopCoroutine(ActivateAbsorbVFX());
		//		}
		//	}
		#endregion
	}

	void VFXHandler()
	{
		if (IsSelected)
		{
			if (CanBeAbsorbed)
			{
				if (!isSelectionVFXPlaying)
				{
					StartCoroutine(ActivateSelectionVFX());
					ToggleOtherSelectionVFX(true);
				}
				if (IsAbsorbing && shouldAbsorbVFXPlay)
				{
					if (!isAbsorbVFXPlaying)
					{
						ToggleAbsorbVFX(true);
					}
				}
				else if(!IsAbsorbing)
				{
					ToggleAbsorbVFX(false);
				}
			}
		}
		else if (!IsSelected)
		{
			ToggleOtherSelectionVFX(false);
			if (isSelectionVFXPlaying)
			{
				StopCoroutine(ActivateSelectionVFX());
				ToggleOtherSelectionVFX(false);
				isSelectionVFXPlaying = false;
			}
			if (isAbsorbVFXPlaying)
			{
				ToggleAbsorbVFX(false);
			}
		}
	}

	public bool HasEnergyLeft()
	{
		if (Energy <= 0)
		{
			return false;
		}
		return true;
	}

	public float AbsorbObject()
	{
		if (IsSelected && CanBeAbsorbed)
		{
			absorptionRate += Time.deltaTime * 1.5f;
			Energy -= absorptionRate;
			float energyPercent = Energy / maxEnergy;
			//AbsorbMeter.fillAmount = energyPercent;
			IsAbsorbing = true;
			if (!HasEnergyLeft())
			{
				IsAbsorbing = false;
				CanBeAbsorbed = false;
				//Destroy(gameObject);
			}
			return absorptionRate;
		}
		IsAbsorbing = false;
		return 0;
	}

	public void InteractWith()
	{
		AbsorbedEnergy = AbsorbObject();
	}

	public void ToggleOtherSelectionVFX(bool val)
	{
		foreach (GameObject vfx in OtherVFX)
		{
			if (val)
				vfx.GetComponent<ParticleSystem>().Play();
			else
				vfx.GetComponent<ParticleSystem>().Stop();
		}
	}

	public void ToggleAbsorbVFX(bool val)
	{
		switch (val)
		{
			case true:
				isAbsorbVFXPlaying = true;
				StartCoroutine(ActivateAbsorbVFX());
				break;
			case false:
				isAbsorbVFXPlaying = false;
				StopCoroutine(ActivateAbsorbVFX());
				break;
		}
	}

	public IEnumerator ActivateSelectionVFX()
	{
		isSelectionVFXPlaying = true;
		while (IsSelected)
		{
			SelectedVFX.GetComponent<ParticleFollowPath>().Activate();
			yield return new WaitForSeconds(SelectedVFX.GetComponent<ParticleFollowPath>().TimeToFinish);
		}
	}

	public IEnumerator ActivateAbsorbVFX()
	{
		while (IsAbsorbing)
		{
			GameObject vfx = Instantiate(AbsorbVFX, transform.position, Quaternion.identity);
			vfx.GetComponent<ParticleFollowPath>().Activate();
			yield return new WaitForSeconds(0.15f);
			vfx.GetComponent<ParticleFollowPath>().StopParticleFollow();
			Destroy(vfx, 5f);
		}
		yield return new WaitForEndOfFrame();
	}
}
