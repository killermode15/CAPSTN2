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

	public List<Absorbable> Absorbables;
	public AbsorbMode CurrentMode;


	private List<Absorbable> allAbsorbable;
	private GameObject currentSelected;
	private int selectedIndex;
	private bool recentlySwitched;
	private bool isAbsorbingCorruption;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (selectedIndex > Absorbables.Count)
			selectedIndex = 0;

		GetAllAbsorbable();
		if (IsSelecting())
		{
			SwitchMode();
			SwitchObject();
			SelectAbsorbable();
			if (currentSelected)
			{
				AbsorbObject();
			}
		}
		if (!IsSelecting() && currentSelected)
		{
			Debug.Log("An object is still selected and the player is not selecting");
			Absorbable selected = currentSelected.GetComponent<Absorbable>();
			selected.IsSelected = false;
			currentSelected = null;
			GetComponent<PlayerController>().CanMove = true;

		}
	}

	public void AbsorbObject()
	{


		Absorbable selected;
		switch (CurrentMode)
		{
			case AbsorbMode.Corruption:
				selected = currentSelected.GetComponent<AbsorbableCorruption>();
				if (selected)
				{
					Debug.Log("Is Selected");
					if (!selected.IsBeingAbsorbed && InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy) && selected.CanBeAbsorbed())
					{
						Debug.Log("Is Absorbing");
						selected.IsBeingAbsorbed = true;
						isAbsorbingCorruption = true;
						GetComponent<PlayerController>().CanMove = false;
					}
					else if(!selected.IsBeingAbsorbed)
					{
						isAbsorbingCorruption = false;
						GetComponent<PlayerController>().CanMove = true;
					}
				}
				else if(selected == null)
				{
					isAbsorbingCorruption = false;
					GetComponent<PlayerController>().CanMove = true;
				}

				break;
			case AbsorbMode.Element:
				if (InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy))
				{
					selected = currentSelected.GetComponent<AbsorbableObject>();
					Element element = GetComponent<UseSkill>().ElementalSkills.Find(x => x.Type == ((AbsorbableObject)selected).Type);
					selected.InteractWith();
					selected.IsBeingAbsorbed = true;
					element.AddEnergy(selected.AbsorbRate);
				}
				break;
		}

	}

	public void GetAllAbsorbable()
	{
		if (allAbsorbable == null)
		{
			allAbsorbable = GameObject.FindObjectsOfType<Absorbable>().ToList();
		}

		List<Absorbable> visibleAbsorbableObjects = new List<Absorbable>();

		foreach (Absorbable obj in allAbsorbable)
		{
			if (!obj)
				continue;
			Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);
			bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

			if (onScreen)
			{
				if (CurrentMode == AbsorbMode.Corruption && obj.EnergyType == EnergyType.Corruption)
				{
					visibleAbsorbableObjects.Add(obj);
				}
				else if (CurrentMode == AbsorbMode.Element && obj.EnergyType == EnergyType.Element)
				{
					visibleAbsorbableObjects.Add(obj);
				}
			}
		}
		Absorbables = visibleAbsorbableObjects;
	}

	public Absorbable GetNearestAbsorbable()
	{
		GameObject nearest = (Absorbables.Count > 0) ? Absorbables[0].gameObject : null;

		if (!nearest)
			return null;

		for (int i = 0; i < Absorbables.Count; i++)
		{
			if (Absorbables[i].HasEnergyLeft())
			{
				if (Absorbables[i].gameObject == nearest)
					continue;

				//The distance of the object and player
				float distBetweenPlayer = (transform.position - Absorbables[i].transform.position).magnitude;
				//The distance between the current nearest object and the player
				float distBetweenNearest = (transform.position - nearest.transform.position).magnitude;

				if (distBetweenPlayer < distBetweenNearest)
				{
					//Set the current object to the nearest object.
					nearest = Absorbables[i].gameObject;
					selectedIndex = i;
				}

			}
		}
		return nearest.GetComponent<Absorbable>();
	}

	public void SelectAbsorbable()
	{
		if (!currentSelected)
		{
			currentSelected = GetNearestAbsorbable().gameObject;
		}

		if (currentSelected)
		{
			Absorbable selected = currentSelected.GetComponent<Absorbable>();
			if (selected.HasEnergyLeft())
			{
				Absorbable currSelected = selected;
				currentSelected = Absorbables[selectedIndex].gameObject;

				if (currSelected != currentSelected)
				{
					Debug.Log("There is a new selected object");
					currSelected.IsSelected = false;
					currentSelected.GetComponent<Absorbable>().IsSelected = true;
				}
				else
				{
					Debug.Log("This is the same object");
					currentSelected.GetComponent<Absorbable>().IsSelected = true;
				}
			}
		}
	}

	public void UpdateCurrentSelected()
	{
		if (currentSelected)
		{
			Absorbable selected = currentSelected.GetComponent<Absorbable>();
			if (!selected.HasEnergyLeft())
			{
				Debug.Log("Object has no more energy left");
				selectedIndex = 0;
				selected.IsSelected = false;
				currentSelected = GetNearestAbsorbable().gameObject;
			}
		}
	}

	public void SwitchMode()
	{
		if (InputManager.Instance.GetKeyDown(ControllerInput.SwitchAbsorbMode) && !isAbsorbingCorruption)
		{
			if (CurrentMode == AbsorbMode.Element)
				CurrentMode = AbsorbMode.Corruption;
			else if (CurrentMode == AbsorbMode.Corruption)
				CurrentMode = AbsorbMode.Element;
		}
	}

	public void SwitchObject()
	{
		if (IsSelecting() && !IsAbsorbing())
		{
			int inputSelection = (int)Input.GetAxisRaw("DPadX");

			if (!recentlySwitched)
			{
				recentlySwitched = true;
				selectedIndex += inputSelection;
				if (selectedIndex > Absorbables.Count)
					selectedIndex = 0;
				else if (selectedIndex < 0)
					selectedIndex = Absorbables.Count-1;
			}

		}
	}

	public bool IsSelecting()
	{
		return Input.GetButton("LeftTrigger") && Absorbables.Count > 0;
	}

	public bool IsAbsorbing()
	{
		return InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy) && IsSelecting();
	}
}
