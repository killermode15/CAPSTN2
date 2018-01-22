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

public class PlayerAnimation : MonoBehaviour {

	public Animator PlayerAnimator;

	private bool isAttacking;
	private bool isJumping;
	private bool isWalking;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayAnimation(PlayerAnimationState state)
	{

	}

	public float GetCurrentAnimationLength()
	{
		return PlayerAnimator.GetCurrentAnimatorStateInfo(0).length;
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
