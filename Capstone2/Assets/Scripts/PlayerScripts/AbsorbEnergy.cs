using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbEnergy : MonoBehaviour
{

	public List<AbsorbableObject> AbsorbableObjects;
	public bool IsSelectingObject;

	private Image AbsorbMeter;
	private AbsorbableObject currentSelectedObject;
	private int selectedIndex;

	private bool recentlySwitched;
	// Use this for initialization
	void Start()
	{
		//List<AbsorbableObject> absorbableObjects = GameObject.FindObjectsOfType<AbsorbableObject>().ToList();
		//AbsorbableObjects = absorbableObjects;
		//currentSelectedObject = AbsorbableObjects[0];
	}

	// Update is called once per frame
	void Update()
	{
		GetAllAbsorbableObjects();
		if (IsCurrentlySelecting())
		{
			UpdateCurrentSelectedObject();
			SelectObject();
			SwitchBetweenObjects();
			if (currentSelectedObject)
			{
				Absorb();
			}
		}

		if (!IsCurrentlySelecting() && currentSelectedObject)
		{
			currentSelectedObject.IsSelected = false;
			currentSelectedObject = null;
		}

		#region old code
		/*
		if (IsCurrentlySelecting())
		{
			currentSelectedObject = AbsorbableObjects[selectedIndex];
			if (currentSelectedObject)
			{
				currentSelectedObject.IsSelected = true;
				//if (Input.GetButton("Cross"))
				if (InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy))
				{
					//GetComponent<Energy>().AddEnergy(50);
					//AbsorbableObjects.Remove(currentSelectedObject);
					//Destroy(currentSelectedObject);
					//return;
					if (!currentSelectedObject.HasEnergyLeft())
						return;
					currentSelectedObject.IsAbsorbing = true;
					currentSelectedObject.InteractWith();
					GetComponent<Energy>().AddEnergy(currentSelectedObject.AbsorbedEnergy);
				}
				else
					currentSelectedObject.IsAbsorbing = false;
				Debug.Log(currentSelectedObject.name);
			}
		}
		else
		{
			if (currentSelectedObject)
			{
				currentSelectedObject.IsSelected = false;
			}
		}

		UpdateSelectionUI();*/
		#endregion
	}

	//Used for when the player is just starting to select objects,
	//this function is called and selects the nearest object
	//as the default to absorb
	GameObject FindNearestAbsorbableObject()
	{
		//The nearest object (default if the first in the list)
		GameObject nearestObject = (AbsorbableObjects.Count > 0) ? AbsorbableObjects[0].gameObject : null;

		//If there is no nearest object, return null
		if (!nearestObject)
			return null;

		//Loop through all objects
		for (int i = 0; i < AbsorbableObjects.Count; i++)
		{
			if (AbsorbableObjects[i].CanBeAbsorbed)
			{
				//If the object is the same as the current nearest object
				if (AbsorbableObjects[i].gameObject == nearestObject)
					//Continue to the next iteration
					continue;

				//The distance of the object and player
				float distBetweenPlayer = (transform.position - AbsorbableObjects[i].transform.position).magnitude;
				//The distance between the current nearest object and the player
				float distBetweenNearest = (transform.position - nearestObject.transform.position).magnitude;

				//If the distance of the object to the player is lesser than the 
				//distance of the current nearest object to the player
				if (distBetweenPlayer < distBetweenNearest)
				{
					//Set the current object to the nearest object.
					nearestObject = AbsorbableObjects[i].gameObject;
					selectedIndex = i;
				}
			}
		}

		return nearestObject;
	}

	void GetAllAbsorbableObjects()
	{
		List<AbsorbableObject> absorbableObjects = GameObject.FindObjectsOfType<AbsorbableObject>().ToList();
		List<AbsorbableObject> visibleAbsorbableObjects = new List<AbsorbableObject>();
		foreach (AbsorbableObject absorbable in absorbableObjects)
		{
			Vector3 screenPoint = Camera.main.WorldToViewportPoint(absorbable.transform.position);
			bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

			if(onScreen && absorbable.CanBeAbsorbed)
			{
				visibleAbsorbableObjects.Add(absorbable);
			}
		}
		AbsorbableObjects = visibleAbsorbableObjects;
	}

	void SelectObject()
	{
		if (!currentSelectedObject)
			currentSelectedObject = FindNearestAbsorbableObject().GetComponent<AbsorbableObject>();

		if (currentSelectedObject)
		{
			if (currentSelectedObject.CanBeAbsorbed)
			{
				//Save the currently selected object
				AbsorbableObject currObject = currentSelectedObject;
				//Then switch to the currentSelectedObject to a new selected object using selectedIndex
				currentSelectedObject = AbsorbableObjects[selectedIndex];

				if (currObject != currentSelectedObject)
				{
					currObject.IsSelected = false;
					currentSelectedObject.IsSelected = true;
				}
				else
				{
					currentSelectedObject.IsSelected = true;
				}
			}
		}
	}

	void SwitchBetweenObjects()
	{
		if (!currentSelectedObject.IsAbsorbing)
		{
			int inputSelection = (int)Input.GetAxisRaw("DPadX");
			Debug.Log(inputSelection);

			switch (inputSelection)
			{
				case 1:
					if (!recentlySwitched)
					{
						selectedIndex++;
						if (selectedIndex > AbsorbableObjects.Count - 1)
							selectedIndex = 0;
						else if (selectedIndex < 0)
							selectedIndex = AbsorbableObjects.Count - 1;
					}
					recentlySwitched = true;
					break;
				case -1:
					if (!recentlySwitched)
					{
						selectedIndex--;
						if (selectedIndex > AbsorbableObjects.Count - 1)
							selectedIndex = 0;
						else if (selectedIndex < 0)
							selectedIndex = AbsorbableObjects.Count - 1;
					}
					recentlySwitched = true;
					break;
				default:
					recentlySwitched = false;
					break;
			}
		}
	}

	void Absorb()
	{
		if(InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy))
		{
			//currentSelectedObject.InteractWith();
			GetComponent<Energy>().AddEnergy(currentSelectedObject.AbsorbObject());
		}
	}

	void UpdateCurrentSelectedObject()
	{
		if(currentSelectedObject)
		{
			if(!currentSelectedObject.CanBeAbsorbed)
			{
				//AbsorbableObjects.Remove(currentSelectedObject);
				selectedIndex = 0;
				currentSelectedObject.IsSelected = false;
				currentSelectedObject = FindNearestAbsorbableObject().GetComponent<AbsorbableObject>();
			}

		}
	}

	bool IsCurrentlySelecting()
	{
		IsSelectingObject = (Input.GetButton("LeftTrigger") && AbsorbableObjects.Count > 0);
		return IsSelectingObject;
	}
}
