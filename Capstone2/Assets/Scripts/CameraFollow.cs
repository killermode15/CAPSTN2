using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	//Player?
	public Transform Player;
	public float CameraFollowSpeed;
	public Vector2 Bounds = new Vector2(2.0f, 1.5f);
	public Vector2 Offset;


	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
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

		transform.position = Vector3.Lerp(transform.position, transform.position + delta + new Vector3(Offset.x,Offset.y), Time.deltaTime * CameraFollowSpeed);

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

		transform.position = transform.position + delta + new Vector3(Offset.x, Offset.y);
	}
}
