using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbEnergy : MonoBehaviour
{

	public List<GameObject> AbsorbableObjects;
	public bool IsSelectingObject;

	private GameObject currentSelectedObject;
	private int selectedIndex;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButton("LeftTrigger") && AbsorbableObjects.Count > 0)
		{
			currentSelectedObject = AbsorbableObjects[selectedIndex];
			if (currentSelectedObject)
			{
				if (Input.GetButtonDown("AButton"))
				{
					GetComponent<Energy>().AddEnergy(50);
					AbsorbableObjects.Remove(currentSelectedObject);
					Destroy(currentSelectedObject);
					return;
				}
				Debug.Log(currentSelectedObject.name);
			}
		}
	}

	//GameObject SelectObject()
	//{

	//}
}
