using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimationState
{
	Idle,
	Move,
	Run,
	Jump,
	Attack,
	Skill,
	Absorb
}

public class PlayerAnimation : MonoBehaviour
{

	public Animator PlayerAnimator;

	private bool isAttacking;
	private bool isJumping;
	private bool isWalking;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		PlayerAnimator.SetFloat("WalkSpeed", Mathf.Abs(Input.GetAxis("Horizontal")) + 1);


		
	}

	public void SetBoolAnimParam(string paramName, bool val)
	{
		PlayerAnimator.SetBool(paramName, val);
	}

	public void SetTriggerAnimParam(string paramName)
	{
		PlayerAnimator.SetTrigger(paramName);
	}

	public bool GetBoolAnimParam(string paramName)
	{
		return PlayerAnimator.GetBool(paramName);
	}

	public float GetCurrentAnimationLength()
	{
		return PlayerAnimator.GetCurrentAnimatorStateInfo(0).length;
	}

	public AnimatorStateInfo GetStateInfo()
	{
		return PlayerAnimator.GetCurrentAnimatorStateInfo(0);
	}

	public bool IsStatePlaying(PlayerAnimationState state)
	{
		throw new NotImplementedException("Not yet implemented");
	}

	public bool IsStatePlaying(string name)
	{
		return PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName(name);
	}
}
