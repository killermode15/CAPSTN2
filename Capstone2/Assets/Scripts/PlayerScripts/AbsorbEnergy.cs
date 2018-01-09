using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbEnergy : MonoBehaviour
{

	public List<AbsorbableObject> AbsorbableObjects;
	public bool IsSelectingObject;

	private Image AbsorbMeter;
	private AbsorbableObject currentSelectedObject;
	private int selectedIndex;
	// Use this for initialization
	void Start()
	{

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
				if (Input.GetButton("Cross"))
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
			if (currentSelectedObject)
			{
				GameObject childCanvas = currentSelectedObject.transform.Find("Canvas").gameObject;
				childCanvas.SetActive(true);
				AbsorbMeter = childCanvas.GetComponentInChildren<Image>();
			}
		}
	}

	bool IsCurrentlySelecting()
	{
		return (Input.GetButton("LeftTrigger") && AbsorbableObjects.Count > 0);
	}
}
