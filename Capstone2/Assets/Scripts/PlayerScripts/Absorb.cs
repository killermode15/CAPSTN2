using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Absorb : MonoBehaviour
{

	public enum AbsorbMode
	{
		Element,
		Corruption
	}

	public AbsorbMode CurrentMode;
	public List<Absorbable> Absorbables;
	public Absorbable CurrentAbsorbable;
	//public float AbsorbRange;

	private PlayerAnimation anim;
	private List<Absorbable> allAbsorbables;
	private int currentSelectedIndex;
	private bool recentlyChangedMode;
	private bool recentlyChangedIndex;
	private bool isAbsorbingCorruption;

	private void OnDrawGizmosSelected()
	{
		//Gizmos.DrawWireSphere(transform.position, AbsorbRange);
	}

	private void Start()
	{
		Absorbables = new List<Absorbable>();
		allAbsorbables = GameObject.FindObjectsOfType<Absorbable>().ToList();
		anim = GetComponent<PlayerAnimation>();
		//StartCoroutine(ChangeAbsorbMode());
	}

	private void Update()
	{

		if (!GetComponent<PlayerController>().CanMove && !Input.GetButton("LeftTrigger") && !anim.GetBoolAnimParam("IsAbsorbing") && !PauseManager.Instance.IsPaused)
		{
			GetComponent<PlayerController>().CanMove = true;
		}

		if (anim.GetBoolAnimParam("IsAbsorbing"))
		{
			if (CurrentMode == AbsorbMode.Element && !IsAbsorbing())
				anim.SetBoolAnimParam("IsAbsorbing", false);
			else if (CurrentMode == AbsorbMode.Corruption && !isAbsorbingCorruption)
				anim.SetBoolAnimParam("IsAbsorbing", false);
		}

		SelectObject();
		if (IsAbsorbing())
			AbsorbObject();
		else if (isAbsorbingCorruption && !Input.GetButton("LeftTrigger"))
		{
			isAbsorbingCorruption = false;
		}
		GetCurrentAbsorbableMode();
		SwitchAbsorbable(currentSelectedIndex);
		SwitchAbsorbMode();
		ChangeIndex();
	}

	private bool IsSelecting()
	{
		if (Input.GetButton("LeftTrigger") && Absorbables.Count > 0)//&& !IsAbsorbing())
		{
			anim.SetBoolAnimParam("IsAbsorbing", true);
			return true;
		}

		anim.SetBoolAnimParam("IsAbsorbing", false);
		return false;
	}
	private bool IsAbsorbing()
	{
		if (!CurrentAbsorbable)
			return false;
		return ((InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy) && Absorbables.Count > 0 && CurrentAbsorbable.HasEnergyLeft()));
	}
	private void ChangeIndex()
	{
		Debug.Log(Input.GetAxisRaw("DPadX"));
		if (IsSelecting())
		{
			if (!recentlyChangedIndex)
			{
				recentlyChangedIndex = true;

				if (Input.GetAxisRaw("DPadX") > 0)
				{
					currentSelectedIndex--;
					if (currentSelectedIndex < 0)
					{
						currentSelectedIndex = Absorbables.Count - 1;
					}
				}
				else if (Input.GetAxisRaw("DPadX") < 0)
				{
					currentSelectedIndex++;
					if (currentSelectedIndex > Absorbables.Count - 1)
					{
						currentSelectedIndex = 0;
					}
				}
			}
			else if (recentlyChangedIndex)
			{
				if (Input.GetAxisRaw("DPadX") == 0)
				{
					recentlyChangedIndex = false;
				}
			}
		}
	}
	private void GetCurrentAbsorbableMode()
	{
		List<Absorbable> absorbableObjects = GameObject.FindObjectsOfType<Absorbable>().ToList();
		List<Absorbable> visibleAbsorbableObjects = new List<Absorbable>();
		foreach (Absorbable absorbable in absorbableObjects)
		{
			Vector3 screenPoint = Camera.main.WorldToViewportPoint(absorbable.transform.position);
			bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
			Debug.Log("Absorbable [" + absorbable.name + "] Location [" + screenPoint + "] Is On Screen [" + onScreen + "]");
			if (onScreen && absorbable.CanBeAbsorbed() && (int)absorbable.EnergyType == (int)CurrentMode)
			{
				visibleAbsorbableObjects.Add(absorbable);
			}
		}

		if (CurrentAbsorbable && (IsAbsorbing() || isAbsorbingCorruption))
		{
			if (visibleAbsorbableObjects [currentSelectedIndex]) {
				if (CurrentAbsorbable != visibleAbsorbableObjects [currentSelectedIndex]) {
					currentSelectedIndex = visibleAbsorbableObjects.FindIndex(x => CurrentAbsorbable);
				}
			}
		}

		Absorbables = visibleAbsorbableObjects;
	}
	private void SwitchAbsorbable(int index)
	{
		if (Absorbables.Count > 0 && (index >= 0 && index < Absorbables.Count) && (IsAbsorbing() || IsSelecting()))
		{
			if (CurrentAbsorbable)
			{
				CurrentAbsorbable.IsSelected = false;
				//CurrentAbsorbable.IsBeingAbsorbed = false;
			}
			CurrentAbsorbable = Absorbables[index];
			CurrentAbsorbable.IsSelected = true;
		}
		else
		{
			if (CurrentAbsorbable)
			{
				CurrentAbsorbable.IsSelected = false;
				CurrentAbsorbable.IsBeingAbsorbed = false;
			}
			CurrentAbsorbable = null;
		}
	}
	private void SwitchAbsorbMode()
	{
		if (!isAbsorbingCorruption)
		{
			if (InputManager.Instance.GetKeyDown(ControllerInput.SwitchAbsorbMode) && Input.GetButton("LeftTrigger") && !recentlyChangedMode)
			{
				if (CurrentAbsorbable)
				{
					CurrentAbsorbable.IsSelected = false;
					CurrentAbsorbable.IsBeingAbsorbed = false;
				}
				recentlyChangedMode = true;
				if (CurrentMode == AbsorbMode.Corruption)
				{
					CurrentMode = AbsorbMode.Element;
					Debug.Log("Current Selected Mode: " + CurrentMode);
				}
				else if (CurrentMode == AbsorbMode.Element)
				{
					CurrentMode = AbsorbMode.Corruption;
					Debug.Log("Current Selected Mode: " + CurrentMode);
				}
			}
			else
				recentlyChangedMode = false;
		}
	}
	private void SelectObject()
	{
		if (CurrentAbsorbable)
		{
			if (IsSelecting() || IsAbsorbing())
			{
				CurrentAbsorbable.IsSelected = true;
			}
			else
			{
				CurrentAbsorbable.IsSelected = false;
				CurrentAbsorbable = null;
			}
		}
	}
	private void AbsorbObject()
	{
		if (CurrentAbsorbable)
		{
			if (CurrentMode == AbsorbMode.Element)
			{
				AbsorbableObject obj = CurrentAbsorbable as AbsorbableObject;
				if (obj)
				{
					if (!Input.GetButton("LeftTrigger"))
					{
						//obj.IsBeingAbsorbed = false;
						//SendMessage("SetCanMove", true);
						GetComponent<PlayerController>().CanMove = true;
						////anim.SetBoolAnimParam("IsAbsorbing", false);
					}
					else if (obj.CanBeAbsorbed())
					{
						Element element = GetComponent<UseSkill>().ElementalSkills.Find(x => x.Type == obj.Type);
						obj.InteractWith();
						obj.IsBeingAbsorbed = true;
						Debug.Log("Is being absorbed " + obj.IsBeingAbsorbed);
						element.AddEnergy(obj.AbsorbRate);
						SendMessage("SetCanMove", false);
						Debug.Log("Absorbing " + obj.name);
						////anim.SetBoolAnimParam("IsAbsorbing", true);
					}
				}
			}
			else if (CurrentMode == AbsorbMode.Corruption)
			{
				AbsorbableCorruption obj = CurrentAbsorbable as AbsorbableCorruption;
				if (obj)
				{

					if (obj.CanBeAbsorbed() && !obj.IsBeingAbsorbed)
					{
						isAbsorbingCorruption = true;
						obj.IsBeingAbsorbed = true;
						anim.SetBoolAnimParam("IsAbsorbing", true);
						//SendMessage("SetCanMove", false);
						GetComponent<PlayerController>().CanMove = false;
					}
					else if (!Input.GetButton("LeftTrigger")) // && !obj.IsSelected)
					{
						//SendMessage("SetCanMove", true);
						GetComponent<PlayerController>().CanMove = true;
						Debug.Log("Can Move: " + GetComponent<PlayerController>().CanMove);
						anim.SetBoolAnimParam("IsAbsorbing", false);
					}
				}
			}
		}
	}
}
