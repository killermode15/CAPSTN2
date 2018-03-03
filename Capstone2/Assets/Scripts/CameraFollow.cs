using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform Player;
	public float CameraFollowSpeed;
	public Vector3 Bounds = new Vector3(2.0f, 1.5f, -5.0f);
	public Vector3 Offset;
	public float CameraNudgeValue;


	// Use this for initialization
	void Start ()
	{
		
	}

	// Update is called once per frame
	void LateUpdate ()
	{
		//Move ();
		Vector3 delta = Vector3.zero;

		float dx = Player.position.x - transform.position.x;
		if(dx > Bounds.x || dx < -Bounds.x)
		{
			if(transform.position.x < Player.position.x)
			{
				delta.x = dx - Bounds.x;
			}
			else
			{
				delta.x = dx + Bounds.x;
			}
		}
		float dy = Player.position.y - transform.position.y;
		if (dy > Bounds.y || dy < -Bounds.y)
		{
			if (transform.position.y < Player.position.y)
			{
				delta.y = dy - Bounds.y;
			}
			else
			{
				delta.y = dy + Bounds.y;
			}
		}
		float dz = Player.position.z - transform.position.z;
		if (dz > Bounds.z || dz < -Bounds.z)
		{
			if (transform.position.z < Player.position.z)
			{
				delta.z = dz - Bounds.z;
			}
			else
			{
				delta.z = dz + Bounds.z;
			}
		}
		Vector3 camPos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		transform.position = 
			Vector3.Lerp(transform.position, camPos + delta + new Vector3(Offset.x,Offset.y, Offset.z) + new Vector3(CameraNudgeValue * Input.GetAxis("Horizontal"), 0), Time.deltaTime * CameraFollowSpeed);

	}

	private void OnValidate()
	{
		Vector3 delta = Vector3.zero;

		float dx = Player.position.x - transform.position.x;
		if (dx > Bounds.x || dx < -Bounds.x)
		{
			if (transform.position.x < Player.position.x)
			{
				delta.x = dx - Bounds.x;
			}
			else
			{
				delta.x = dx + Bounds.x;
			}
		}
		float dy = Player.position.y - transform.position.y;
		if (dy > Bounds.y || dy < -Bounds.y)
		{
			if (transform.position.y < Player.position.y)
			{
				delta.y = dy - Bounds.y;
			}
			else
			{
				delta.y = dy + Bounds.y;
			}
		}
		float dz = Player.position.z - transform.position.z;
		if (dz > Bounds.z || dz < -Bounds.z)
		{
			if (transform.position.z < Player.position.z)
			{
				delta.z = dz - Bounds.z;
			}
			else
			{
				delta.z = dz + Bounds.z;
			}
		}

		transform.position = transform.position + delta + new Vector3(Offset.x, Offset.y, Offset.z);
	}
}
