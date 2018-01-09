using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour {

	public List<Element> ElementalSkills;
	public Element ActiveElement;
	
	private List<Element> elementsOnCooldown;
	private List<float> currentCooldowns;

	private int currentActiveElementIndex;

	// Use this for initialization
	void Start () {

		foreach (Element element in ElementalSkills)
			element.IsOnCooldown = false;

		ActiveElement = ElementalSkills[currentActiveElementIndex];
		elementsOnCooldown = new List<Element>();
		currentCooldowns = new List<float>();
	}
	
	// Update is called once per frame
	void Update () {

		UpdateElementCooldowns();

		if(Input.GetButtonDown("RightTrigger"))
		{
			if (!ActiveElement.IsElementUnlocked || ActiveElement.IsOnCooldown ||GetComponent<Energy>().CurrentEnergy < ActiveElement.EnergyCost)
				return;
			UseActiveElement();
		}
		if(Input.GetButtonDown("LeftBumper"))
		{
			SelectNextElement();
		}
	}

	public void SelectNextElement()
	{
		currentActiveElementIndex++;
		if(currentActiveElementIndex >= ElementalSkills.Count)
		{
			currentActiveElementIndex = 0;
		}
		ActiveElement = ElementalSkills[currentActiveElementIndex];
	}
	
	public void UseActiveElement()
	{
		if (!ActiveElement.IsOnCooldown)
		{
			ActiveElement.Use();
			ActiveElement.IsOnCooldown = true;
			elementsOnCooldown.Add(ActiveElement);
			currentCooldowns.Add(ActiveElement.CooldownDuration);
		}
	}

	public void UpdateElementCooldowns()
	{
		for(int i = 0; i < currentCooldowns.Count; i++)
		{
			currentCooldowns[i] -= Time.deltaTime;
			if(currentCooldowns[i] <= 0)
			{
				currentCooldowns.RemoveAt(i);
				elementsOnCooldown[i].IsOnCooldown = false;
				elementsOnCooldown.RemoveAt(i);
				return;
			}
		}
	}
}
