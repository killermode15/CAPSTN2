using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

	public float Health;
	public float MaxHealth;

	public Slider HealthBar;

	private float hpPercent;
	private float dampTime = 5f;
	private float currentLerpTime;

	public void Start()
	{
		Health = MaxHealth;
		if (!HealthBar)
		{
			HealthBar = GameObject.FindGameObjectWithTag("HPBar").GetComponent<Slider>();
		}
	}

	public void Update()
	{
		//HP Display
		hpPercent = Health / MaxHealth;
		if (HealthBar.value != hpPercent)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > dampTime)
			{
				currentLerpTime = dampTime;
			}

			HealthBar.value = Mathf.Lerp(HealthBar.value, hpPercent, currentLerpTime / dampTime);
		}
		else
			currentLerpTime = 0;

		//Damage debugger
		if (Input.GetKeyDown(KeyCode.Space))
			RemoveHealth(50);

	}

	// Use this for initialization
	public void AddHealth(float val)
	{
		Health += val;
		if (Health > MaxHealth)
			Health = MaxHealth;
		//currentLerpTime = 0;
	}

	// Update is called once per frame
	public void RemoveHealth(float val)
	{
		Health -= val;
		if (Health < 0)
			Health = 0;
		//currentLerpTime = 0;
	}
}
