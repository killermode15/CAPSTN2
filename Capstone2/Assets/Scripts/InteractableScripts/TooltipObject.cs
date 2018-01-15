using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipObject : MonoBehaviour, IInteractable {

	[TextArea]
	public string Tooltip;
	public GameObject TooltipGameObject;
	public TextMeshProUGUI TooltipTextUI;

	private Animator TextUIAnimator;
	private bool isTooltipActive;

	// Use this for initialization
	void Start () {
		TextUIAnimator = TooltipGameObject.GetComponent<Animator>();
		TooltipGameObject.SetActive(false);

		TooltipTextUI.text = Tooltip;

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void InteractWith()
	{
		StopAllCoroutines();
		StartCoroutine(ActivateTooltipOnAnimationFinished("OpenTooltipTextUI", TooltipTextUI.gameObject, true));
	}

	IEnumerator ActivateTooltipOnAnimationFinished(string animationName, GameObject objectToActivate, bool val)
	{
		//while(	TextUIAnimator.GetCurrentAnimatorClipInfo(0).Length < 0.9f &&
		//		TextUIAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
		//{
		//	yield return new WaitForEndOfFrame();
		//}
		yield return new WaitForSeconds(TextUIAnimator.GetCurrentAnimatorStateInfo(0).length);
		objectToActivate.SetActive(val);
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			TooltipGameObject.SetActive(true);
			InteractWith();
			TextUIAnimator.SetBool("IsTooltipOpen", true);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			TextUIAnimator.SetBool("IsTooltipOpen", false);
			TooltipTextUI.gameObject.SetActive(false);
			StopAllCoroutines();
			StartCoroutine(ActivateTooltipOnAnimationFinished("CloseTooltipTextUI", TooltipGameObject, false));
		}
	}
}
