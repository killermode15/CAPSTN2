using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public float OrbitSpeed = 4f;
	public float FollowSpeed = 4f;
	public float RadiusOffset;
	public Vector3 Offset;

	private GameObject player;
	private GameObject target;
	private float currentAngle;
	private Vector3 camOffset;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		camOffset = transform.position - player.transform.position;
	}

	private void LateUpdate()
	{
		float orbitValue = Input.GetAxis("RightStickX");

		Quaternion camTurnAngle = Quaternion.AngleAxis(orbitValue * OrbitSpeed, Vector3.up);
		camOffset = camTurnAngle * camOffset;

		Vector3 newPos = player.transform.position + camOffset;

		transform.position = Vector3.Slerp(transform.position, newPos, Time.deltaTime * FollowSpeed);
		transform.LookAt(player.transform);

		//transform.position = player.transform.position + (transform.position - player.transform.position).normalized * RadiusOffset;
		//transform.RotateAround(player.transform.position, Vector3.up, orbitValue * OrbitSpeed * Time.deltaTime);
	}

	Vector3 GetDirFromAngle(float angleInDegrees)
	{

		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
