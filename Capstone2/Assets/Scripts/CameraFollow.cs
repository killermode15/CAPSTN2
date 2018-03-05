using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public float OrbitSpeed = 4f;
	public float FollowSpeed = 4f;
	public Vector3 Offset;
	public Vector2 CameraYLimit = new Vector2(23, 35);

	private GameObject player;
	private GameObject target;
	private Vector3 camOffset;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		camOffset = transform.position - player.transform.position;
	}

	private void LateUpdate()
	{
		float orbitValue = Input.GetAxis("RightStickX");
		float panValue= Input.GetAxis("RightStickY");

		Quaternion camTurnAngle = Quaternion.AngleAxis(orbitValue * OrbitSpeed, Vector3.up);
		Quaternion camPanAngle = Quaternion.AngleAxis(panValue * OrbitSpeed, transform.right);

		camOffset = camPanAngle * camTurnAngle * camOffset;

		Vector3 origPos = new Vector3(camOffset.x, camOffset.y, camOffset.z);
		camOffset += transform.TransformDirection(Offset);
		Vector3 newPos = player.transform.position + camOffset;

		transform.position = Vector3.Slerp(transform.position, newPos, Time.deltaTime * FollowSpeed);
		transform.LookAt(player.transform);

		camOffset = origPos;
	}

	Vector3 GetDirFromAngle(float angleInDegrees)
	{

		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
