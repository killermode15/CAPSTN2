using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDownInidcator : MonoBehaviour {

	public Image CoolDownBar;

	private UseSkill useSkillRef;

	private float maxCoolDown;
	private float dampTime = 5f;
	private float currentLerpTime;

	// Use this for initialization
	void Start () {
		useSkillRef = GetComponent<UseSkill>();
	}

	// Update is called once per frame
	void Update () {

		//Debug.Log ("cool down duration: " + useSkillRef.ActiveElement.CooldownDuration);

		Element activeElement = useSkillRef.ActiveElement;
		int elementCooldownIndex = useSkillRef.elementsOnCooldown.FindIndex (x => x == activeElement);
		bool isElementOnCooldown = activeElement.IsOnCooldown;
		float currentCooldown = (isElementOnCooldown) ? useSkillRef.currentCooldowns[elementCooldownIndex] : 0;

		maxCoolDown = currentCooldown / useSkillRef.ActiveElement.CooldownDuration;

		//Debug.Log (maxCoolDown);
		//if(isElementOnCooldown)
			//CoolDownBar.fillAmount = Mathf.Lerp(CoolDownBar.fillAmount, 0, currentCooldown / useSkillRef.ActiveElement.CooldownDuration);
		CoolDownBar.fillAmount = maxCoolDown;
		/*
		if (CoolDownBar.fillAmount != maxCoolDown)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > dampTime)
			{
				currentLerpTime = dampTime;
			}

			CoolDownBar.fillAmount = Mathf.Lerp(CoolDownBar.fillAmount, 0, maxCoolDown);
		}
		else
			currentLerpTime = 0;*/
	}
}
