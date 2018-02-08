using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementEnergyUI : MonoBehaviour {

	public Image EnergyBar;

	private UseSkill useSkillRef;

	private float energyPercent;
	private float dampTime = 5f;
	private float currentLerpTime;

	// Use this for initialization
	void Start () {
		useSkillRef = GetComponent<UseSkill>();
	}
	
	// Update is called once per frame
	void Update () {

		energyPercent = useSkillRef.ActiveElement.CurrentUseableEnergy / useSkillRef.ActiveElement.MaxUseableEnergy;
		EnergyBar.GetComponent<Image>().color = useSkillRef.ActiveElement.EnergyColor;
		if (EnergyBar.fillAmount != energyPercent)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > dampTime)
			{
				currentLerpTime = dampTime;
			}

			EnergyBar.fillAmount = Mathf.Lerp(EnergyBar.fillAmount, energyPercent, currentLerpTime / dampTime);
		}
		else
			currentLerpTime = 0;
	}
}
