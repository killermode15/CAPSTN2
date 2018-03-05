using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPausable
{
	[Header("Movement")]
	public float MoveSpeed = 6.0f;
	public float JumpHeight;
	public float RollSpeed;
	public float RollDuration;

	[Space]
	[Header("Movement Values")]
	public float RollCooldown;
	public float TurnSmoothTime = 0.02f;

	[Space]
	[Header("Prefabs")]
	public GameObject JumpVFX;

	[HideInInspector] public PlayerAnimation anim;
	[HideInInspector] public bool InDialogue = false;
	[HideInInspector] public bool CanRoll = true;
	[HideInInspector] public bool CanJump = true;
	[HideInInspector] public bool CanMove = true;

	private bool canDoubleJump;
	private float rollValue;
	private float rollSpeed;
	private float initialRollValue;
	private float turnSmoothVel;
	private float origZPos;
	private float currentRotateTo;
	private Vector3 moveDirection;
	private Vector3 rollDirection;
	private CharacterController controller;


	void Start()
	{
		PauseManager.Instance.addPausable(this);

		controller = GetComponent<CharacterController>();
		anim = GetComponent<PlayerAnimation>();

		controller.detectCollisions = true;

		origZPos = transform.position.z;
	}

	void OnDisable()
	{
		PauseManager.Instance.removePausable(this);
	}

	void Update()
	{

		CalculateGravity();
		RotateCharacter();
		Jump();
		Move();

		if (InputManager.Instance.GetKeyDown(ControllerInput.Move) && CanMove)
		{
			Roll();
		}
		UpdateRollAnim();


		if (controller.collisionFlags == CollisionFlags.Above)
		{
			moveDirection.y = 0;
		}
	}

	#region Gravity

	//Calculates the y velocity depending on whether the player is jumping
	//or is falling
	void CalculateGravity()
	{
		if (IsGrounded() && !InDialogue)
		{
			CanJump = true;
			moveDirection.y = (Physics.gravity.y * Time.deltaTime);
		}
		else
		{
			if (!IsGrounded())
				CanJump = false;
			if (!CanJump || moveDirection.y > 0)
			{
				if (!IsGrounded())
					moveDirection.y += (Physics.gravity.y * Time.deltaTime) * 2;
			}

			if (!CanJump && moveDirection.y < 0)
			{
				if (!IsGrounded())
					moveDirection.y += Physics.gravity.y * Time.deltaTime;
			}
		}
		anim.SetBoolAnimParam("HasLanded", IsGrounded());
	}

	#endregion

	#region Movement

	#region Dash
	public void Roll()
	{
		if (CanRoll)
		{

			if (rollDirection.magnitude == 0)
			{
				anim.SetBoolAnimParam("IsRolling", true);
				initialRollValue = RollDuration;
				rollValue = RollDuration;

				rollDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
				rollDirection.Normalize();
				rollDirection *= RollSpeed;
				StartCoroutine(DoRollDuration());
				StartCoroutine(StartRollCooldown());
			}
		}
	}

	void UpdateRollAnim()
	{
		if (anim.GetBoolAnimParam("IsRolling") && anim.GetStateInfo().IsName("Roll"))
		{
			if (anim.GetStateInfo().normalizedTime >= 0.95f || !anim.GetStateInfo().IsName("Roll"))
			{
				anim.SetBoolAnimParam("IsRolling", false);
			}
		}
	}

	IEnumerator DoRollDuration()
	{
		yield return new WaitForSeconds(RollDuration);
		rollDirection = Vector3.zero;
	}

	IEnumerator StartRollCooldown()
	{
		CanRoll = false;
		yield return new WaitForSeconds(RollCooldown);
		CanRoll = true;
	}

	#endregion

	#region Jump

	//Adds a value to the y velocity of the movement vector
	public void DoubleJump(float val)
	{
		JumpVFX.GetComponent<ParticleSystem>().Play();
		moveDirection.y = 0;
		moveDirection.y += val;
		CanJump = false;
	}

	//Launches the player when pressing the jump button
	void Jump()
	{
		//If the player presses X while not pressing LeftTrigger and can jump
		//if (Input.GetButtonDown("Cross") && !Input.GetButton("LeftTrigger") && canJump)
		if (InputManager.Instance.GetKeyDown(ControllerInput.Jump) && !InputManager.Instance.GetKey(ControllerInput.TriggerElementWheel)
			&& !InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy))
		{
			if (CanJump)
			{
				anim.SetTriggerAnimParam("Jump");
				//Set the y velocity to the specified jump height
				moveDirection.y = JumpHeight;
				//And set canJump to false
				CanJump = false;
				canDoubleJump = true;
			}
			else if (canDoubleJump)
			{
				JumpVFX.GetComponent<ParticleSystem>().Play();
				moveDirection.y = 0;
				moveDirection.y += JumpHeight;
				CanJump = false;
				canDoubleJump = false;
			}
		}
	}

	#endregion

	#region Move

	//Move the character based on the move direction
	//Y velocity is placed in a separate variable to
	//prevent being multiplied by the move speed
	void Move()
	{
		//Get the current y velocity of the movement direction
		float currY = moveDirection.y;
		//Then get the movement input from the player
		if (CanMove)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

			//Multiply it to the movespeed
			moveDirection *= MoveSpeed;

			//Add dash if turned onx
			//float dash = (initialRollValue == 0) ? 0 : rollValue / initialRollValue;
			//moveDirection.x += DashCurve.Evaluate(dash) * rollSpeed;
			if (rollDirection.magnitude > 0)
			{
				moveDirection += rollDirection;
			}

			#region Code for Jumping with Momentum
			//else
			//{
			//	Vector3 currentInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			//	currentInput *= 0.25f;
			//	moveDirection += currentInput;
			//	moveDirection = Vector3.ClampMagnitude(moveDirection, 20);
			//}
			#endregion
		}
		else
		{
			moveDirection = Vector3.zero;
		}

		moveDirection = Camera.main.transform.TransformDirection(moveDirection);

		//Then set the y velocity back
		moveDirection.y = currY;


		controller.Move(moveDirection * Time.deltaTime);

		if (IsGrounded() && !InDialogue)
		{
			moveDirection.y = 0;
			CanJump = true;
		}
	}

	#endregion

	#endregion

	//Rotates the character based on the direction of movement
	void RotateCharacter()
	{
		//Get the normalized movement direction
		Vector3 moveDir = moveDirection.normalized;

		if (moveDir.x != 0 && moveDir.z != 0)
			currentRotateTo = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;

		if (moveDir.magnitude != 0)
		{
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, currentRotateTo, ref turnSmoothVel, TurnSmoothTime);
		}
	}

	#region Checkers

	//Checks if the character is grounded
	public bool IsGrounded()
	{
		return controller.isGrounded;
	}

	#endregion

	#region Set Functions

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

	#endregion

	#region Pause/Unpause

	public void Pause()
	{
		CanMove = false;
		CanJump = false;
		GetComponent<PlayerAnimation>().canAnimate = false;
	}

	public void UnPause()
	{
		CanMove = true;
		CanJump = true;
		GetComponent<PlayerAnimation>().canAnimate = true;
	}

	#endregion
}
