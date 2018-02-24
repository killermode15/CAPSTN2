using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSelectionUI : MonoBehaviour
{

	public List<Transform> ElementUIList;
	public float SmoothDampTime = 0.02f;
	public UseSkill UseSkillScriptRef;

	[HideInInspector]
	public static bool isSelecting { get; set; }
	private bool isDoneSwitching;
	private float smoothDampVel;
	private float currentRotateTo;

	// Use this for initialization
	void Start()
	{
		foreach (Transform child in transform)
		{
			ElementUIList.Add(child);
		}
		UseSkillScriptRef = GameObject.FindGameObjectWithTag("Player").GetComponent<UseSkill>() ;
	}

	// Update is called once per frame
	void Update()
	{
		//if (Input.GetButtonDown("LeftBumper"))
		if (InputManager.Instance.GetKeyDown(ControllerInput.TriggerElementWheel))
		{
			isSelecting = true;
		}
		else if (InputManager.Instance.GetKeyUp(ControllerInput.TriggerElementWheel))
		{
			isSelecting = false;
		}

		SelectUI();
		SwitchElement();
	}

	void SelectUI()
	{
		foreach (Transform element in ElementUIList)
		{
			if (isSelecting)
			{
				if (!element.gameObject.activeSelf)
					element.gameObject.SetActive(true);
				element.GetComponent<Animator>().SetBool("IsSelecting", isSelecting);
			}
			else
			{
				if (element.gameObject.activeSelf)
				{
					element.GetComponent<Animator>().SetBool("IsSelecting", isSelecting);
					if (element.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length >= 0.9f && element.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CloseSelectionUI"))
						element.gameObject.SetActive(false);
				}
			}
		}
	}

	void SwitchElement()
	{
		if (isSelecting)
		{
			Time.timeScale = 0.25f;
			Vector2 input = new Vector2((int)Input.GetAxisRaw("RightStickX") * -1, (int)Input.GetAxisRaw("RightStickY"));
			input.Normalize();
			if (input.magnitude > 0)
			{
				currentRotateTo = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
			}
			if ((transform.eulerAngles.z != currentRotateTo))
				transform.eulerAngles = Vector3.forward * Mathf.SmoothDampAngle(transform.eulerAngles.z, currentRotateTo, ref smoothDampVel, SmoothDampTime * Time.unscaledDeltaTime);
		}
		else
		{
			Time.timeScale = 1f;
			switch ((int)currentRotateTo)
			{
				case 0:
					//Fire
					UseSkillScriptRef.SwitchElement(ElementType.Fire);
					break;
				case 90:
					UseSkillScriptRef.SwitchElement(ElementType.Wind);
					//Wind
					break;
				case 180:
					UseSkillScriptRef.SwitchElement(ElementType.Water);
					//Water
					break;
				case -90:
					UseSkillScriptRef.SwitchElement(ElementType.Earth);
					//Earth
					break;
			}
		}
	}
}
