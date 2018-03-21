using UnityEngine;
using System.Collections;


namespace TMPro.Examples
{

	public class CameraController : MonoBehaviour
	{
		public enum CameraModes { Follow, Isometric, Free }

		[Header("Right Stick Variables")]
		public bool InvertCameraControlY;
		public float OrbitSpeed;
		public float PanSpeed;
		public Vector3 Offset;
		public Vector2 ElevationLimit;

		[Space]
		[Header("Targetting Variables")]
		public float CenterTargetZoomDistance;
		public Transform centeredTarget;

		private GameObject player;
		private Transform originalTarget;
		private float originalZoomInValue;


		[Space]
		[Header("Original Variables")]
		public Transform CameraTarget;

		public float FollowDistance = 30.0f;
		public float MaxFollowDistance = 100.0f;
		public float MinFollowDistance = 2.0f;

		public float ElevationAngle = 30.0f;
		public float MaxElevationAngle = 85.0f;
		public float MinElevationAngle = 0f;

		public float OrbitalAngle = 0f;

		public CameraModes CameraMode = CameraModes.Follow;

		public bool MovementSmoothing = true;
		public bool RotationSmoothing = false;
		private bool previousSmoothing;

		public float MovementSmoothingValue = 25f;
		public float RotationSmoothingValue = 5.0f;

		public float MoveSensitivity = 2.0f;

		private Vector3 currentVelocity = Vector3.zero;
		private Vector3 desiredPosition;
		private float mouseX;
		private float mouseY;
		private Vector3 moveVector;
		private float mouseWheel;

		private Transform cameraTransform;
		private Transform dummyTarget;

		// Controls for Touches on Mobile devices
		//private float prev_ZoomDelta;


		private const string event_SmoothingValue = "Slider - Smoothing Value";
		private const string event_FollowDistance = "Slider - Camera Zoom";


		void Awake()
		{
			if (QualitySettings.vSyncCount > 0)
				Application.targetFrameRate = 60;
			else
				Application.targetFrameRate = -1;

			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
				Input.simulateMouseWithTouches = false;

			cameraTransform = transform;
			previousSmoothing = MovementSmoothing;
		}


		// Use this for initialization
		void Start()
		{
			if (CameraTarget == null)
			{
				// If we don't have a target (assigned by the player, create a dummy in the center of the scene).
				dummyTarget = new GameObject("Camera Target").transform;
				CameraTarget = dummyTarget;
			}
			else
			{
				originalZoomInValue = FollowDistance;
				player = CameraTarget.transform.parent.gameObject;
				originalTarget = CameraTarget;
			}
		}

		// Update is called once per frame
		void LateUpdate()
		{
			GetPlayerInput();
			

			// Check if we still have a valid target
			if (CameraTarget != null)
			{
				if (CameraMode == CameraModes.Isometric)
				{
					desiredPosition = CameraTarget.position + Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * new Vector3(0, 0, -FollowDistance);
				}
				else if (CameraMode == CameraModes.Follow)
				{
					desiredPosition = CameraTarget.position + CameraTarget.TransformDirection(Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * (new Vector3(0, 0, -FollowDistance)));
				}
				else
				{
					// Free Camera implementation

					#region Lock On Behaviour

					GameObject playerTarget = player.GetComponent<LockOn>().currentTarget;

					Transform target = (playerTarget != null) ? playerTarget.transform : null;
					if (target)
					{
						Vector3 center = originalTarget.transform.parent.position + target.position;
						center /= 2;

						centeredTarget.position = center;
						float distance = Vector3.Distance(player.transform.position, target.position);
						FollowDistance = originalZoomInValue + (originalZoomInValue * (distance / (2 * CenterTargetZoomDistance)));
						Vector3 dir = new Vector3((target.position - player.transform.position).x, 0, (target.position - player.transform.position).z);

						float angle = Vector3.Angle(dir, Vector3.forward);

						Vector3 direction = target.position - player.transform.position;

						float dot = Vector3.Dot(direction.normalized, player.transform.position.normalized);

						Vector3 relativePoint = transform.InverseTransformPoint(target.position);
						float angleFromWorldZAxis = Vector3.SignedAngle(player.transform.position, target.position, Vector3.forward);
						//Debug.Log(angleFromWorldZAxis);

						float desiredAngle = 0;

						if (dot > 0.0f)
						{
							if (angleFromWorldZAxis < 0.0f)
							{
								desiredAngle = angle;
							}
							else
							{
								desiredAngle = angle * -1;
							}
						}
						else if (dot < 0.0f)
						{
							if (angleFromWorldZAxis < 0.0f)
							{
								desiredAngle = angle;
							}
							else
							{
								desiredAngle = angle * -1;
							}
						}
						OrbitalAngle = Mathf.Lerp(OrbitalAngle, desiredAngle, Time.deltaTime * 50);

						OrbitalAngle += Input.GetAxisRaw("RightStickX") * OrbitSpeed * Time.deltaTime;

						if (InvertCameraControlY)
							ElevationAngle += Input.GetAxisRaw("RightStickY") * -1 * PanSpeed * Time.deltaTime;
						else
							ElevationAngle += Input.GetAxisRaw("RightStickY") * PanSpeed * Time.deltaTime;

						ElevationAngle = Mathf.Clamp(ElevationAngle, ElevationLimit.x, ElevationLimit.y);

						desiredPosition = CameraTarget.position + Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * new Vector3(0, 0, -FollowDistance);
						desiredPosition += transform.TransformDirection(Offset);

					}
					#endregion
					#region Normal Camera Behaviour
					else
					{
						FollowDistance = originalZoomInValue;
						if (CameraTarget != originalTarget)
							CameraTarget = originalTarget;
						//Debug.Log("TEST");
						#region Reset Camera Position

						if (Input.GetKeyDown(KeyCode.Joystick1Button11))
						{
							float angle = Vector3.Angle(player.transform.forward, Vector3.forward);
							float angleFromWorldZAxis = Vector3.SignedAngle(player.transform.position, player.transform.forward, Vector3.forward);

							if (angleFromWorldZAxis < 0.0f)
							{
								Debug.Log("[Angle: " + angle + "] [Angle From Z: " + angleFromWorldZAxis + "] Facing Left");
								OrbitalAngle = angle;
							}
							else
							{
								Debug.Log("[Angle: " + angle + "] [Angle From Z: " + angleFromWorldZAxis + "] Facing Right");
								OrbitalAngle = angle * -1;
							}
						}
						#endregion


						#region Panning and Rotation Behaviour

						OrbitalAngle += Input.GetAxisRaw("RightStickX") * OrbitSpeed * Time.deltaTime;

						if (InvertCameraControlY)
							ElevationAngle += Input.GetAxisRaw("RightStickY") * -1 * PanSpeed * Time.deltaTime;
						else
							ElevationAngle += Input.GetAxisRaw("RightStickY") * PanSpeed * Time.deltaTime;

						ElevationAngle = Mathf.Clamp(ElevationAngle, ElevationLimit.x, ElevationLimit.y);


						#endregion
						desiredPosition = CameraTarget.position + Quaternion.Euler(ElevationAngle, OrbitalAngle, 0f) * new Vector3(0, 0, -FollowDistance);
						desiredPosition += transform.TransformDirection(Offset);
					}
					#endregion


				}

				if (MovementSmoothing == true)
				{
					// Using Smoothing
					cameraTransform.position = Vector3.SmoothDamp(cameraTransform.position, desiredPosition, ref currentVelocity, MovementSmoothingValue * Time.fixedDeltaTime);
					//cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, Time.deltaTime * 5.0f);
				}
				else
				{
					// Not using Smoothing
					cameraTransform.position = desiredPosition;
				}

				if (RotationSmoothing == true)
					cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.LookRotation(CameraTarget.position - cameraTransform.position), RotationSmoothingValue * Time.deltaTime);
				else
				{
					//GameObject playerTarget = player.GetComponent<LockOn>().currentTarget;

					//Transform target = (playerTarget != null) ? playerTarget.transform : null;
					//if (target)
					//	cameraTransform.LookAt(target);
					//else
						cameraTransform.LookAt(CameraTarget);
				}

			}

		}



		void GetPlayerInput()
		{
			moveVector = Vector3.zero;

			// Check Mouse Wheel Input prior to Shift Key so we can apply multiplier on Shift for Scrolling
			mouseWheel = Input.GetAxis("Mouse ScrollWheel");

			float touchCount = Input.touchCount;

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || touchCount > 0)
			{
				mouseWheel *= 10;

				if (Input.GetKeyDown(KeyCode.I))
					CameraMode = CameraModes.Isometric;

				if (Input.GetKeyDown(KeyCode.F))
					CameraMode = CameraModes.Follow;

				if (Input.GetKeyDown(KeyCode.S))
					MovementSmoothing = !MovementSmoothing;


				// Check for right mouse button to change camera follow and elevation angle
				if (Input.GetMouseButton(1))
				{
					mouseY = Input.GetAxis("Mouse Y");
					mouseX = Input.GetAxis("Mouse X");

					if (mouseY > 0.01f || mouseY < -0.01f)
					{
						ElevationAngle -= mouseY * MoveSensitivity;
						// Limit Elevation angle between min & max values.
						ElevationAngle = Mathf.Clamp(ElevationAngle, MinElevationAngle, MaxElevationAngle);
					}

					if (mouseX > 0.01f || mouseX < -0.01f)
					{
						OrbitalAngle += mouseX * MoveSensitivity;
						if (OrbitalAngle > 360)
							OrbitalAngle -= 360;
						if (OrbitalAngle < 0)
							OrbitalAngle += 360;
					}
				}

				// Get Input from Mobile Device
				if (touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

					// Handle elevation changes
					if (deltaPosition.y > 0.01f || deltaPosition.y < -0.01f)
					{
						ElevationAngle -= deltaPosition.y * 0.1f;
						// Limit Elevation angle between min & max values.
						ElevationAngle = Mathf.Clamp(ElevationAngle, MinElevationAngle, MaxElevationAngle);
					}


					// Handle left & right 
					if (deltaPosition.x > 0.01f || deltaPosition.x < -0.01f)
					{
						OrbitalAngle += deltaPosition.x * 0.1f;
						if (OrbitalAngle > 360)
							OrbitalAngle -= 360;
						if (OrbitalAngle < 0)
							OrbitalAngle += 360;
					}

				}

				// Check for left mouse button to select a new CameraTarget or to reset Follow position
				if (Input.GetMouseButton(0))
				{
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					if (Physics.Raycast(ray, out hit, 300, 1 << 10 | 1 << 11 | 1 << 12 | 1 << 14))
					{
						if (hit.transform == CameraTarget)
						{
							// Reset Follow Position
							OrbitalAngle = 0;
						}
						else
						{
							CameraTarget = hit.transform;
							OrbitalAngle = 0;
							MovementSmoothing = previousSmoothing;
						}

					}
				}


				if (Input.GetMouseButton(2))
				{
					if (dummyTarget == null)
					{
						// We need a Dummy Target to anchor the Camera
						dummyTarget = new GameObject("Camera Target").transform;
						dummyTarget.position = CameraTarget.position;
						dummyTarget.rotation = CameraTarget.rotation;
						CameraTarget = dummyTarget;
						previousSmoothing = MovementSmoothing;
						MovementSmoothing = false;
					}
					else if (dummyTarget != CameraTarget)
					{
						// Move DummyTarget to CameraTarget
						dummyTarget.position = CameraTarget.position;
						dummyTarget.rotation = CameraTarget.rotation;
						CameraTarget = dummyTarget;
						previousSmoothing = MovementSmoothing;
						MovementSmoothing = false;
					}


					mouseY = Input.GetAxis("Mouse Y");
					mouseX = Input.GetAxis("Mouse X");

					moveVector = cameraTransform.TransformDirection(mouseX, mouseY, 0);

					dummyTarget.Translate(-moveVector, Space.World);

				}

			}

			// Check Pinching to Zoom in - out on Mobile device
			if (touchCount == 2)
			{
				Touch touch0 = Input.GetTouch(0);
				Touch touch1 = Input.GetTouch(1);

				Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
				Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

				float prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude;
				float touchDelta = (touch0.position - touch1.position).magnitude;

				float zoomDelta = prevTouchDelta - touchDelta;

				if (zoomDelta > 0.01f || zoomDelta < -0.01f)
				{
					FollowDistance += zoomDelta * 0.25f;
					// Limit FollowDistance between min & max values.
					FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
				}


			}

			// Check MouseWheel to Zoom in-out
			if (mouseWheel < -0.01f || mouseWheel > 0.01f)
			{

				FollowDistance -= mouseWheel * 5.0f;
				// Limit FollowDistance between min & max values.
				FollowDistance = Mathf.Clamp(FollowDistance, MinFollowDistance, MaxFollowDistance);
			}


		}
	}
}