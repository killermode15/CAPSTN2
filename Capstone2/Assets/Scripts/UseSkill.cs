using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSkill : MonoBehaviour {

	public List<Element> ElementalSkills;
	public Element ActiveElement;

	private int currentActiveElementIndex;

	// Use this for initialization
	void Start () {
		ActiveElement = ElementalSkills[currentActiveElementIndex];
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("RightTrigger"))
		{
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
		ActiveElement.Use();
	}
}
