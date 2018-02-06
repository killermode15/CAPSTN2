using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool CanMove = true;
	public float DashSpeed;
	public float MoveSpeed = 6.0f;
	public float JumpHeight;
	public float TurnSmoothTime = 0.02f;
	public AnimationCurve DashCurve;
	//public float FallMultiplier;
	//public float LowJumpMultiplier;

	private bool canJump = true;
	private float dashValue;
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
		//Set controller to detect collisions
		controller.detectCollisions = true;

		canJump = true;

		origZPos = transform.position.z;
	}
	void Update()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, origZPos);
		
		Move();
		Jump();
		if (dashValue > 0.15f || dashValue < -0.15f)
		{
			dashValue -= Time.deltaTime;
		}
		else
		{
			dashValue = 0;
			initialDashVal = 0;
		}
	}

	private void LateUpdate()
	{

		CalculateGravity();
		RotateCharacter();
	}

	//Calculates the y velocity depending on whether the player is jumping
	//or is falling
	void CalculateGravity()
	{
		#region old code
		//transform.position = new Vector3(transform.position.x, transform.position.y, origZPos);

		//if (IsGrounded())
		//{
		//	canJump = true;
		//	//Set the y velocity to 0
		//	moveDirection.y = 0;
		//	Debug.Log("grounded");

		//}
		//else
		//{
		//	Debug.Log("not grounded");
		//	if (moveDirection.y < 0)
		//	{
		//		moveDirection.y += Physics.gravity.y * FallMultiplier * Time.deltaTime;
		//	}
		//	else if (moveDirection.y > 0 && !Input.GetButton("Cross"))
		//	{
		//		moveDirection.y += Physics.gravity.y * LowJumpMultiplier * Time.deltaTime;
		//	}
		//	moveDirection.y += Physics.gravity.y * Time.deltaTime;

		//}
		#endregion

		canJump = IsGrounded();

		if (!canJump || moveDirection.y > 0)
		{
			moveDirection.y += (Physics.gravity.y * Time.deltaTime) * 2;
		}

		if(moveDirection.y < 0)
		{
			moveDirection.y += Physics.gravity.y * Time.deltaTime;
		}

		else if (canJump)
		{
			//If the player is grounded
			if (IsGrounded())
				//Set the y velocity to 0
				moveDirection.y = 0;
		}

		#region old code
		////If the player is currently jumping or is not grounded
		//if (!IsGrounded())
		//{
		//	if (!canJump)
		//		moveDirection.y += Physics.gravity.y * Time.deltaTime;
		//	else
		//	{
		//		canJump = true;
		//		moveDirection.y = 0;
		//	}//if (moveDirection.y < 0 || moveDirection.y > 0)
		//	//{
		//	//	//Subtract gravity (per frame) from the y velocity
		//	//}
		//}
		////If the player is grounded
		//else if (IsGrounded())
		//{
		//	//Set the y velocity to 0
		//	moveDirection.y = 0;
		//	canJump = true;
		//}
		//if (!IsGrounded())
		//	canJump = false;
		#endregion
	}

	//Launches the player when pressing the jump button
	void Jump()
	{
		//If the player presses X while not pressing LeftTrigger and can jump
		//if (Input.GetButtonDown("Cross") && !Input.GetButton("LeftTrigger") && canJump)
		if (InputManager.Instance.GetKeyDown(ControllerInput.Jump) && !InputManager.Instance.GetKey(ControllerInput.TriggerElementWheel)
			&& !InputManager.Instance.GetKey(ControllerInput.AbsorbEnergy) && canJump)
		{
			//Set the y velocity to the specified jump height
			moveDirection.y = JumpHeight;
			//And set canJump to false
			canJump = false;
		}
	}

	//Move the character based on the move direction
	//Y velocity is placed in a separate variable to
	//prevent being multiplied by the move speed
	void Move()
	{
		//Get the current y velocity of the movement direction
		float currY = moveDirection.y;
		//Then get the movement input from the player
		if (CanMove)
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		else
			moveDirection = Vector3.zero;
		//Currently commented out because movement
		//is based on where the character is facing
		//moveDirection = transform.TransformDirection(moveDirection);

		//Multiply it to the movespeed
		moveDirection *= MoveSpeed;
		//Add dash if turned onx
		float dash = (initialDashVal == 0) ? 0 : dashValue / initialDashVal;
		moveDirection.x += DashCurve.Evaluate(dash) * DashSpeed ;
		Debug.Log(DashCurve.Evaluate(dash));
		//Then set the y velocity back
		moveDirection.y = currY;

		controller.Move(moveDirection * Time.deltaTime);

		if (IsGrounded())
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

		//if (moveDir.x != 0)
		//{
		//	float targetRotation = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
		//	transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, TurnSmoothTime);
		//}

		if (moveDir.x != 0)
		{
			currentRotateTo = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
		}
		if (transform.eulerAngles.y != currentRotateTo)
		{
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, currentRotateTo, ref turnSmoothVel, TurnSmoothTime);
		}
	}

	//Adds a value to the y velocity of the movement vector
	public void AddJumpVelocity(float val)
	{
		moveDirection.y = 0;
		moveDirection.y += val;
	}

	public void AddForwardVelocity(float duration)
	{
		if (dashValue <= 0)
		{
			initialDashVal = duration;
			dashValue = duration * Input.GetAxisRaw("Horizontal");
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
	}
}
