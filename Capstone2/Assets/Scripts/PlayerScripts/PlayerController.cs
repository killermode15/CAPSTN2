using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPausable
{
	public GameObject JumpVFX;
	public bool CanMove;
	//public float DashSpeed;
	public float MoveSpeed = 6.0f;
	public float JumpHeight;
	public float TurnSmoothTime = 0.02f;
	public AnimationCurve DashCurve;
	//public float FallMultiplier;
	//public float LowJumpMultiplier;
	[HideInInspector]
	public PlayerAnimation anim;
	public bool inDialogue;

	public bool canJump = true;
	private float dashValue;
	private float dashSpeed;
	private float initialDashVal;
	private float turnSmoothVel;
	private float origZPos;
	private float currentRotateTo;
	private Vector3 moveDirection;
	private CharacterController controller;


	void Start()
	{
		//Get reference to character controller
		controller = GetComponent<CharacterController>();
		anim = GetComponent<PlayerAnimation>();
		//Set controller to detect collisions
		controller.detectCollisions = true;

		CanMove = true;
		canJump = true;
		PauseManager.Instance.addPausable (this);
		origZPos = transform.position.z;

		inDialogue = false;
	}

	void OnDisable(){
		PauseManager.Instance.removePausable (this);
	}

	void Update()
	{
		//Debug.Log ("canJump: " + canJump);
		transform.position = new Vector3(transform.position.x, transform.position.y, origZPos);

		CalculateGravity();
		RotateCharacter();
		Jump();
		Move();
		if (dashValue > 0.15f || dashValue < -0.15f)
		{
			dashValue -= Time.deltaTime;
		}
		else
		{
			dashValue = 0;
			initialDashVal = 0;
		}

		if (controller.collisionFlags == CollisionFlags.Above)
		{
			Debug.Log("I hit my head");
			moveDirection.y = 0;
		}
	}

	private void LateUpdate()
	{

		//Debug.Log ("Late CanMove: " + CanMove);
		//Debug.Log ("Late canJump: " + canJump);
	}

	//Calculates the y velocity depending on whether the player is jumping
	//or is falling
	void CalculateGravity()
	{
		if (IsGrounded() && !inDialogue)
		{
			canJump = true;
			moveDirection.y = (Physics.gravity.y * Time.deltaTime);
		}
		else
		{
			if (!IsGrounded())
				canJump = false;
			if (!canJump || moveDirection.y > 0)
			{
				if (!IsGrounded())
					moveDirection.y += (Physics.gravity.y * Time.deltaTime) * 2;
			}

			if (!canJump && moveDirection.y < 0)
			{
				if (!IsGrounded())
					moveDirection.y += Physics.gravity.y * Time.deltaTime;
			}
		}
		anim.SetBoolAnimParam("HasLanded", IsGrounded());
	}

	//Launches the player when pressing the jump button
	void Jump()
	{
		//If the player presses X while not pressing LeftTrigger and can jump
		//if (Input.GetButtonDown("Cross") && !Input.GetButton("LeftTrigger") && canJump)
		if (InputManager.Instance.GetKeyDown(ControllerInput.Jump) && !InputManager.Instance.GetKey(ControllerInput.TriggerElementWheel)
			&& !InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy) && canJump)
		{
			anim.SetTriggerAnimParam("Jump");
			//Set the y velocity to the specified jump height
			moveDirection.y = JumpHeight;
			//And set canJump to false
			canJump = false;
		}
	}

	//Adds a value to the y velocity of the movement vector
	public void AddJumpVelocity(float val)
	{
		JumpVFX.GetComponent<ParticleSystem>().Play();
		moveDirection.y = 0;
		moveDirection.y += val;
		canJump = false;
	}

	//Move the character based on the move direction
	//Y velocity is placed in a separate variable to
	//prevent being multiplied by the move speed
	void Move()
	{
		//Get the current y velocity of the movement direction
		float currY = moveDirection.y;
		//Then get the movement input from the player
		if (CanMove) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);

			//Multiply it to the movespeed
			moveDirection *= MoveSpeed;
			//Add dash if turned onx
			float dash = (initialDashVal == 0) ? 0 : dashValue / initialDashVal;
			moveDirection.x += DashCurve.Evaluate (dash) * dashSpeed;

		} else {
			moveDirection = Vector3.zero;
		}
		//Currently commented out because movement
		//is based on where the character is facing
		//moveDirection = transform.TransformDirection(moveDirection);

		//Then set the y velocity back
		moveDirection.y = currY;


		controller.Move(moveDirection * Time.deltaTime);

		if (IsGrounded() && !inDialogue)
		{
			moveDirection.y = 0;
			canJump = true;
		}
	}

	//Rotates the character based on the direction of movement
	void RotateCharacter()
	{
		//Get the normalized movement direction
		Vector3 moveDir = moveDirection.normalized;

		if (moveDir.x != 0)
		{
			currentRotateTo = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
		}
		if (transform.eulerAngles.y != currentRotateTo)
		{
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, currentRotateTo, ref turnSmoothVel, TurnSmoothTime);
		}
	}


	public void AddForwardVelocity(float duration, float speed)
	{
		if (dashValue <= 0 || dashValue > 0)
		{
			initialDashVal = duration;
			dashValue = duration;
			dashSpeed = speed * Input.GetAxisRaw("Horizontal");
		}
	}

	//Checks if the character is grounded
	public bool IsGrounded()
	{
		return controller.isGrounded;
	}

	public void SetCanMove(bool val)
	{
		CanMove = val;
		Debug.Log("CanMove: " + CanMove);
	}

	public void SetCanMove(int val)
	{
		CanMove = (val == 0) ? false : true;
		Debug.Log("CanMove: " + CanMove);
	}

	public void StopMovement()
	{
		moveDirection = Vector3.zero;
	}

	public void Pause(){
		CanMove = false;
		canJump = false;
		GetComponent<PlayerAnimation> ().canAnimate = false;
	}

	public void UnPause(){
		CanMove = true;
		canJump = true;
		GetComponent<PlayerAnimation> ().canAnimate = true;
	}
}
