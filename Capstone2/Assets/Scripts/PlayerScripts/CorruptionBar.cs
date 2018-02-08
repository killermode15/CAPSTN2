using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionBar : MonoBehaviour
{

	public float CurrentCorruption;
	public float MaxCorruption;
	[Range(1, 10)]
	public int DecayRateMultiplier;
	public float CorruptionReleaseRate;
	public float TimeTillDecayAmplify;
	public float HealthDecayRate;
	[Range(0, 1)]
	public float PercentTillDecay;
	/*
	public float MinPercentTillDecay;
	public float MaxPercentTillDecay;*/

	public Image CorruptionEffect;

	private HP playerHP;
	private float timeSinceDecayStarted;
	private int currentMultiplier;

	private void OnValidate()
	{
		if (TimeTillDecayAmplify < 1)
			TimeTillDecayAmplify = 1;
	}

	// Use this for initialization
	void Start()
	{
		playerHP = GetComponent<HP>();
		currentMultiplier = 1;
	}

	// Update is called once per frame
	void Update()
	{

		VignetteEffect();

		float corruptionPerc = CurrentCorruption / MaxCorruption;
		if (CurrentCorruption > MaxCorruption)
			CurrentCorruption = MaxCorruption;

		if (corruptionPerc >= PercentTillDecay)
		{
			playerHP.RemoveHealth(HealthDecayRate * currentMultiplier * Time.deltaTime);

			if (timeSinceDecayStarted == 0)
			{
				timeSinceDecayStarted = Time.time;
			}

			if (timeSinceDecayStarted + TimeTillDecayAmplify <= Time.time)
			{
				currentMultiplier += DecayRateMultiplier - 1;
				timeSinceDecayStarted = 0;
			}

		}
		else
		{
			currentMultiplier = 1;
			if (timeSinceDecayStarted != 0)
				timeSinceDecayStarted = 0;
		}
	}

	public void ReleaseCorruption()
	{
		if (CurrentCorruption > 0)
			CurrentCorruption -= Time.deltaTime * CorruptionReleaseRate;
		else if (CurrentCorruption < 0)
			CurrentCorruption = 0;
	}

	void VignetteEffect()
	{
		Color effectColor = CorruptionEffect.color;
		effectColor.a = CurrentCorruption / MaxCorruption;
		CorruptionEffect.color = effectColor;
	}
}
