using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Square			= 0
	Cross			= 1
	Circle			= 2
	Triangle		= 3
	
	Left Bumper		= 4
	Right Bumper	= 5
	Left Trigger	= 6
	Right Trigger	= 7
 */

public enum ControllerInput
{
	Attack = (int)KeyCode.Joystick1Button0,
	Jump = (int)KeyCode.Joystick1Button1,
	Move = (int)KeyCode.Joystick1Button2, 
	Defend = (int)KeyCode.Joystick1Button3,

	ActivateAltar = (int)KeyCode.Joystick1Button13,
	TriggerElementWheel = (int)KeyCode.Joystick1Button4,
	ModifyMove = (int)KeyCode.Joystick1Button5,
	StartAbsorb = (int)KeyCode.Joystick1Button6,
	AbsorbEnergy = (int)KeyCode.Joystick1Button6 & Attack,
	UseCurrentElement = (int)KeyCode.Joystick1Button7,
	SelectElement = TriggerElementWheel & (int)KeyCode.Joystick1Button1,
	SwitchAbsorbMode = (int)KeyCode.Joystick1Button0,
}


public class InputManager : MonoBehaviour
{

	private static InputManager instance;

	public static InputManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<InputManager>();
				if (instance == null)
				{
					GameObject newInstance = new GameObject("Input Manager");
					instance = newInstance.AddComponent<InputManager>();
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}


	public bool GetKey(ControllerInput input)
	{
		switch (input)
		{
			case ControllerInput.Attack:
			case ControllerInput.Jump:
			case ControllerInput.Defend:
			case ControllerInput.Move:
			case ControllerInput.TriggerElementWheel:
			case ControllerInput.UseCurrentElement:
			case ControllerInput.ActivateAltar:
			case ControllerInput.StartAbsorb:
			case ControllerInput.ModifyMove:
				if (ReturnAsGetKey(input))
					return true;
				return false;

			case ControllerInput.SelectElement:
				if (Input.GetKey((KeyCode)((int)ControllerInput.TriggerElementWheel)))
				{
					if (Input.GetKey((KeyCode)((int)ControllerInput.Jump)))
						return true;
				}
				return false;

			case ControllerInput.AbsorbEnergy:
				if (Input.GetKey(KeyCode.Joystick1Button6))
				{
					if (Input.GetKey((KeyCode)((int)ControllerInput.Jump)))
						return true;
				}
				return false;

			default:
				return false;

		}
	}

	public bool GetKeyDown(ControllerInput input)
	{
		switch (input)
		{
			case ControllerInput.Attack:
			case ControllerInput.Jump:
			case ControllerInput.Defend:
			case ControllerInput.Move:
			case ControllerInput.TriggerElementWheel:
			case ControllerInput.UseCurrentElement:
			case ControllerInput.ActivateAltar:
			case ControllerInput.StartAbsorb:
			case ControllerInput.ModifyMove:
				if (ReturnAsGetKeyDown(input))
					return true;
				return false;

			case ControllerInput.SelectElement:
				if (Input.GetKeyDown((KeyCode)((int)ControllerInput.TriggerElementWheel)))
				{
					if (Input.GetKeyDown((KeyCode)((int)ControllerInput.Jump)))
						return true;
				}
				return false;

			case ControllerInput.AbsorbEnergy:
				if (Input.GetKeyDown(KeyCode.Joystick1Button6))
				{
					if (Input.GetKeyDown((KeyCode)((int)ControllerInput.Jump)))
						return true;
				}
				return false;

			default:
				return false;

		}
	}

	public bool GetKeyUp(ControllerInput input)
	{
		switch (input)
		{
			case ControllerInput.Attack:
			case ControllerInput.Jump:
			case ControllerInput.Defend:
			case ControllerInput.Move:
			case ControllerInput.TriggerElementWheel:
			case ControllerInput.UseCurrentElement:
			case ControllerInput.ActivateAltar:
			case ControllerInput.StartAbsorb:
			case ControllerInput.ModifyMove:
				if (ReturnAsGetKeyUp(input))
					return true;
				return false;

			case ControllerInput.SelectElement:
				if (Input.GetKeyUp((KeyCode)((int)ControllerInput.TriggerElementWheel)))
				{
					if (Input.GetKeyUp((KeyCode)((int)ControllerInput.Jump)))
						return true;
				}
				return false;

			case ControllerInput.AbsorbEnergy:
				if (Input.GetKeyUp(KeyCode.Joystick1Button6))
				{
					if (Input.GetKeyUp((KeyCode)((int)ControllerInput.Jump)))
						return true;
				}
				return false;

			default:
				return false;

		}
	}

	private bool ReturnAsGetKey(ControllerInput code)
	{
		KeyCode keyCodeInput = (KeyCode)((int)code);
		return Input.GetKey(keyCodeInput);
	}

	private bool ReturnAsGetKeyDown(ControllerInput code)
	{
		KeyCode keyCodeInput = (KeyCode)((int)code);
		return Input.GetKeyDown(keyCodeInput);
	}

	private bool ReturnAsGetKeyUp(ControllerInput code)
	{
		KeyCode keyCodeInput = (KeyCode)((int)code);
		return Input.GetKeyUp(keyCodeInput);
	}


}
