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
		List<AbsorbableObject> absorbableObjects = GameObject.FindObjectsOfType<AbsorbableObject>().ToList();
		AbsorbableObjects = absorbableObjects;
		currentSelectedObject = AbsorbableObjects[0];
	}

	// Update is called once per frame
	void Update()
	{
		if (IsCurrentlySelecting())
		{
			currentSelectedObject = AbsorbableObjects[selectedIndex];
			if (currentSelectedObject)
			{
				currentSelectedObject.IsSelected = true;
				//if (Input.GetButton("Cross"))
				if(InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy))
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

		UpdateSelectionUI();
	}

	void UpdateSelectionUI()
	{
		if (IsCurrentlySelecting())
		{
			if(!recentlySwitched)
			{
				int inputSelection = (int)Input.GetAxisRaw("RightStickX");

				switch(inputSelection)
				{
					case 1:
						selectedIndex++;
						break;
					case -1:
						selectedIndex--;
						break;
				}

				if (selectedIndex > AbsorbableObjects.Count)
					selectedIndex = 0;
				else if(selectedIndex < 0)
					selectedIndex = AbsorbableObjects.Count-1;

				recentlySwitched = true;
			}

			UpdateSelectedObject();
		}
	}

	void UpdateSelectedObject()
	{
		//if (currentSelectedObject)
		{
			if (currentSelectedObject != AbsorbableObjects[selectedIndex])
			{
				GameObject childCanvas = currentSelectedObject.transform.Find("Canvas").gameObject;
				childCanvas.SetActive(false);
				AbsorbMeter = null;

				currentSelectedObject = AbsorbableObjects[selectedIndex];

				childCanvas = currentSelectedObject.transform.Find("Canvas").gameObject;
				childCanvas.SetActive(false);
				AbsorbMeter = null;
			}
		}
	}

	bool IsCurrentlySelecting()
	{
		return (InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy) && AbsorbableObjects.Count > 0);
	}
}
