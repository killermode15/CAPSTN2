using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionBarUI : MonoBehaviour {

	public Image CorruptionBar;

	private CorruptionBar corruptionRef;

	private float energyPercent;
	private float dampTime = 5f;
	private float currentLerpTime;

	// Use this for initialization
	void Start()
	{
		corruptionRef = GetComponent<CorruptionBar>();
	}

	// Update is called once per frame
	void Update()
	{

		energyPercent = corruptionRef.CurrentCorruption / corruptionRef.MaxCorruption;

		if (CorruptionBar.fillAmount != energyPercent)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > dampTime)
			{
				currentLerpTime = dampTime;
			}

			CorruptionBar.fillAmount = Mathf.Lerp(CorruptionBar.fillAmount, energyPercent, currentLerpTime / dampTime);
		}
		else
			currentLerpTime = 0;
	}
}
