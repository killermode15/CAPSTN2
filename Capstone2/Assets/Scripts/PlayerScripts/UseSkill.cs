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
		{
			element.IsOnCooldown = false;
			element.CurrentUseableEnergy = 0;
		}

		ActiveElement = ElementalSkills[currentActiveElementIndex];
		elementsOnCooldown = new List<Element>();
		currentCooldowns = new List<float>();
	}
	
	// Update is called once per frame
	void Update () {

		UpdateElementCooldowns();

		//if(Input.GetButtonDown("RightTrigger"))
		if(InputManager.Instance.GetKey(ControllerInput.UseCurrentElement))
		{
			if (!ActiveElement.IsElementUnlocked || ActiveElement.IsOnCooldown || ActiveElement.CurrentUseableEnergy < ActiveElement.EnergyCost)
				return;
			UsePrimaryActiveElement();
		}
		else if (InputManager.Instance.GetKey(ControllerInput.UseSecondaryCurrentElement))
		{
			if (!ActiveElement.IsElementUnlocked || ActiveElement.IsOnCooldown || ActiveElement.CurrentUseableEnergy < ActiveElement.EnergyCost)
				return;
			UseSecondaryActiveElement();
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

	public void UsePrimaryActiveElement()
	{
		if (!ActiveElement.IsOnCooldown)
		{
			ActiveElement.Use();
			ActiveElement.IsOnCooldown = true;
			elementsOnCooldown.Add(ActiveElement);
			currentCooldowns.Add(ActiveElement.CooldownDuration);
		}
	}
	public void UseSecondaryActiveElement()
	{
		if (!ActiveElement.IsOnCooldown)
		{
			ActiveElement.SecondaryUse();
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

	public void SwitchElement(ElementType element)
	{
		ActiveElement = ElementalSkills.Find(x => x.Type == element);
	}

	public void SetElementOnCooldown(Element element)
	{
		Element elementOnCooldown = ElementalSkills.Find(x => x == element);
		element.IsOnCooldown = true;
		elementsOnCooldown.Add(element);
		currentCooldowns.Add(element.CooldownDuration);
	}
}
