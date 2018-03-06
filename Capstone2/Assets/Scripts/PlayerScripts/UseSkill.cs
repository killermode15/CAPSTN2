using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour
{

	private const int UP = 0;
	private const int DOWN = 1;
	private const int LEFT = 2;
	private const int RIGHT = 3;

	public List<Element> ElementalSkills;
	public Element ActiveElement;

	public delegate void OnSkillUse(Element element);
	public OnSkillUse onSkillUse;

	public List<Element> elementsOnCooldown;
	public List<float> currentCooldowns;

	private int currentActiveElementIndex;

	// Use this for initialization
	void Start()
	{

		foreach (Element element in ElementalSkills)
		{
			element.IsOnCooldown = false;
		}

		ActiveElement = ElementalSkills[currentActiveElementIndex];
		elementsOnCooldown = new List<Element>();
		currentCooldowns = new List<float>();
		StartCoroutine(ChangeElement());
	}

	// Update is called once per frame
	void Update()
	{
		UpdateElementCooldowns();
		
		if (InputManager.Instance.GetKey(ControllerInput.UseCurrentElement))
		{
			if (!ActiveElement.IsElementUnlocked || ActiveElement.IsOnCooldown)
				return;
			UseElement();
		}
	}

	IEnumerator ChangeElement()
	{
		bool isKeyPressed = false;
		int input = 0;
		while (true)
		{
			if (!isKeyPressed)
			{
				if (Input.GetAxis("DPadX") == 1)
				{
					input = RIGHT;
					isKeyPressed = true;
				}
				else if(Input.GetAxis("DPadX") == -1)
				{
					input = LEFT;
					isKeyPressed = true;
				}
				else if (Input.GetAxis("DPadY") == 1)
				{
					input = UP;
					isKeyPressed = true;
				}
				else if (Input.GetAxis("DPadY") == -1)
				{
					input = DOWN;
					isKeyPressed = true;
				}
				if (isKeyPressed)
				{
					switch (input)
					{
						case UP:
							SwitchElement(ElementType.Earth);
							break;
						case DOWN:
							SwitchElement(ElementType.Wind);
							break;
						case LEFT:
							SwitchElement(ElementType.Light);
							break;
						case RIGHT:
							SwitchElement(ElementType.Water);
							break;
					}
				}
			}
			else
			{
				if(Input.GetAxis("DPadX") == 0 && Input.GetAxis("DPadY") == 0)
				{
					isKeyPressed = false;
				}
			}

			yield return new WaitForEndOfFrame();
		}
	}

	public void UseElement()
	{
		if (!ActiveElement.IsOnCooldown)
		{
			if (ActiveElement.Use(gameObject))
			{
				if (ActiveElement.GetType() == typeof(WaterElement) || ActiveElement.GetType() == typeof(EarthElement))
					GetComponentInChildren<ElementEffects>().isCasting = true;
				if (onSkillUse != null)
					onSkillUse.Invoke(ActiveElement);

				ActiveElement.IsOnCooldown = true;
				SetElementOnCooldown(ActiveElement);
			}
		}
	}

	public void UpdateElementCooldowns()
	{
		for (int i = 0; i < currentCooldowns.Count; i++)
		{
			currentCooldowns[i] -= Time.deltaTime;
			if (currentCooldowns[i] <= 0)
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

	public Element GetElement(System.Type elementType)
	{
		return ElementalSkills.Find(x => x.GetType() == elementType);
	}
}
