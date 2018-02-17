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

	private PlayerAnimation anim;
	private List<Absorbable> allAbsorbables;
	private int currentSelectedIndex;
	private bool recentlyChangedMode;
	private bool recentlyChangedIndex;
	private bool isAbsorbingCorruption;

	private void Start()
	{
		Absorbables = new List<Absorbable>();
		allAbsorbables = GameObject.FindObjectsOfType<Absorbable>().ToList();
		anim = GetComponent<PlayerAnimation>();
		//StartCoroutine(ChangeAbsorbMode());
	}

	private void Update()
	{

		AbsorbAnimation();
		SelectObject();
		if (IsAbsorbing())
			AbsorbObject();
		GetCurrentAbsorbableMode();
		SwitchAbsorbable(currentSelectedIndex);
		SwitchAbsorbMode();
		ChangeIndex();
	}

	private bool IsSelecting()
	{
		return Input.GetButton("LeftTrigger") && Absorbables.Count > 0 && !IsAbsorbing();
	}
	private bool IsAbsorbing()
	{
		return InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy) && Absorbables.Count > 0 && CurrentAbsorbable.HasEnergyLeft();
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
					currentSelectedIndex++;
					if (currentSelectedIndex > Absorbables.Count - 1)
					{
						currentSelectedIndex = 0;
						Debug.Log("Select Index: " + currentSelectedIndex);
					}
				}
				else if (Input.GetAxisRaw("DPadX") < 0)
				{
					currentSelectedIndex--;
					if (currentSelectedIndex < 0)
					{
						currentSelectedIndex = Absorbables.Count - 1;
						Debug.Log("Select Index: " + currentSelectedIndex);
					}
				}
			}
			else if (recentlyChangedIndex)
			{
				if (Input.GetAxisRaw("RightStickX") == 0)
				{
					recentlyChangedIndex = false;
				}
			}
		}
	}
	private void GetCurrentAbsorbableMode()
	{
		Absorbables.Clear();
		allAbsorbables.RemoveAll(x => x == null);
		if (CurrentMode == AbsorbMode.Corruption)
		{
			foreach (Absorbable obj in allAbsorbables)
			{
				if (obj.GetType() == typeof(AbsorbableCorruption))
				{
					if (Vector3.Distance(transform.position, obj.transform.position) <= 20)
						Absorbables.Add(obj);
				}
			}
		}
		else if (CurrentMode == AbsorbMode.Element)
		{
			foreach (Absorbable obj in allAbsorbables)
			{
				if (obj.GetType() == typeof(AbsorbableObject))
				{
					Absorbables.Add(obj);
				}
			}
		}
	}
	private void SwitchAbsorbable(int index)
	{
		if (Absorbables.Count > 0 && (index >= 0 && index < Absorbables.Count))
		{
			CurrentAbsorbable = Absorbables[index];
		}
		else
		{
			CurrentAbsorbable = null;
		}
	}
	private void SwitchAbsorbMode()
	{

		if (InputManager.Instance.GetKeyDown(ControllerInput.SwitchAbsorbMode) && Input.GetButton("LeftTrigger") && !recentlyChangedMode)
		{
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
	private void AbsorbAnimation()
	{
		/*
		PlayerAnimation anim = GetComponent<PlayerController>().anim;
		if (CurrentMode == AbsorbMode.Corruption)
		{
			AbsorbableCorruption current = (AbsorbableCorruption)CurrentAbsorbable;
			if (IsSelecting())
			{
				if (current)
				{
					if (current.IsBeingAbsorbed)
						anim.SetBoolAnimParam("IsAbsorbing", true);
					else
						anim.SetBoolAnimParam("IsAbsorbing", false);
				}
				else
					anim.SetBoolAnimParam("IsAbsorbing", false);
			}
			else
				anim.SetBoolAnimParam("IsAbsorbing", false);
		}
		else if (CurrentMode == AbsorbMode.Element)
		{

			if (IsAbsorbing())
			{
				if (!anim.GetBoolAnimParam("IsAbsorbing"))
				{
					anim.SetBoolAnimParam("IsAbsorbing", true);
				}
			}
			else
			{
				if (anim.GetBoolAnimParam("IsAbsorbing"))
				{
					anim.SetBoolAnimParam("IsAbsorbing", false);
				}
			}
		}*/
	}
	private void SelectObject()
	{
		if (CurrentAbsorbable)
		{
			if (IsSelecting())
			{
				CurrentAbsorbable.IsSelected = true;
			}
			else
			{
				CurrentAbsorbable.IsSelected = false;
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
						obj.IsBeingAbsorbed = false;

						//SendMessage("SetCanMove", true);
						GetComponent<PlayerController>().CanMove = true;
						Debug.Log("Can Move: " + GetComponent<PlayerController>().CanMove);
						//anim.SetBoolAnimParam("IsAbsorbing", false);
					}
					else if (obj.CanBeAbsorbed())
					{
						Element element = GetComponent<UseSkill>().ElementalSkills.Find(x => x.Type == obj.Type);
						obj.InteractWith();
						obj.IsBeingAbsorbed = true;
						element.AddEnergy(obj.AbsorbRate);
						//GetComponent<PlayerController>().CanMove = false;
						SendMessage("SetCanMove", false);
						//anim.SetBoolAnimParam("IsAbsorbing", true);
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
						obj.IsBeingAbsorbed = true;
						//anim.SetBoolAnimParam("IsAbsorbing", true);	
						//SendMessage("SetCanMove", false);
						GetComponent<PlayerController>().CanMove = false;
					}
					else if (!Input.GetButton("LeftTrigger")) // && !obj.IsSelected)
					{

						//SendMessage("SetCanMove", true);
						GetComponent<PlayerController>().CanMove = true;
						Debug.Log("Can Move: " + GetComponent<PlayerController>().CanMove);
						//anim.SetBoolAnimParam("IsAbsorbing", false);

					}
				}
			}
		}
	}
}
