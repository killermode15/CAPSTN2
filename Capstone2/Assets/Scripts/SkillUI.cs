using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour {

	public Image SelectedElementImage;

	private UseSkill useSkillScript;

	// Use this for initialization
	void Start () {
		useSkillScript = GetComponent<UseSkill>();
		if(!SelectedElementImage)
		{
			SelectedElementImage = GameObject.FindGameObjectWithTag("SkillUI").GetComponent<Image>();
		}
	}

	// Update is called once per frame
	void Update() {
		if (useSkillScript.ActiveElement)
		{
			SelectedElementImage.sprite = useSkillScript.ActiveElement.ElementIcon;
		}
	}	
}
