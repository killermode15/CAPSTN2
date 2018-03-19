using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

	public Selectable InitialSelectable;

	private EventSystem eventSystem;

	// Use this for initialization
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		eventSystem = EventSystem.current;
		ChangeSelected(InitialSelectable.gameObject);
		StartCoroutine(GetInput());

	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator GetInput()
	{
		bool hasPressedInput = false;
		Vector2 directionalInput = Vector2.zero;
		while (true)
		{
			directionalInput = new Vector2((int)Input.GetAxis("Horizontal"), (int)Input.GetAxis("Vertical"));

			if (GetCurrentSelectable() == null && directionalInput.magnitude != 0)
				ChangeSelected(InitialSelectable.gameObject);
			
			yield return new WaitForEndOfFrame();
		}
	}

	public Selectable GetCurrentSelectable()
	{
		if (eventSystem.currentSelectedGameObject == null)
			return null;
		return eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
	}

	public Selectable GetSelectableFromRoute(string direction)
	{
		switch (direction.ToLower())
		{
			case "up":
				if (GetCurrentSelectable().FindSelectableOnUp())
					return GetCurrentSelectable().FindSelectableOnUp();
				return null;
			case "down":
				if (GetCurrentSelectable().FindSelectableOnDown())
					return GetCurrentSelectable().FindSelectableOnDown();
				return null;
			case "left":
				if (GetCurrentSelectable().FindSelectableOnLeft())
					return GetCurrentSelectable().FindSelectableOnLeft();
				return null;
			case "right":
				if (GetCurrentSelectable().FindSelectableOnRight())
					return GetCurrentSelectable().FindSelectableOnRight();
				return null;
			default:
				Debug.Log("No direction found");
				return null;
		}
	}

	public bool HasSelectionRoute(Selectable selectable, string direction)
	{
		switch (direction.ToLower())
		{
			case "up":
				if (selectable.FindSelectableOnUp())
					return true;
				return false;
			case "down":
				if (selectable.FindSelectableOnDown())
					return true;
				return false;
			case "left":
				if (selectable.FindSelectableOnLeft())
					return true;
				return false;
			case "right":
				if (selectable.FindSelectableOnRight())
					return true;
				return false;
			default:
				Debug.Log("No direction found");
				return false;
		}
	}

	public void ChangeSelected(GameObject newSelected)
	{
		eventSystem.SetSelectedGameObject(newSelected);
	}
}
