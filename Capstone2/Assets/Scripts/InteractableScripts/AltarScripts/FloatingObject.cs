using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{

	public Transform FloatPositionReference;
	public bool UseReference;
	public bool PlayOnAwake;

	public float DampSpeed;
	public float BobbingSpeed;
	public float RotationSpeed;

	private Vector3 originalPosition;
	private bool isActivated;

	// Use this for initialization
	void Start()
	{
		originalPosition = transform.position;
		if (PlayOnAwake)
			isActivated = true;
	}

	// Update is called once per frame
	void Update()
	{

		if (isActivated)
		{
			if (UseReference)
			{
				if (FloatPositionReference)
				{
					Vector3 newPos = new Vector3(FloatPositionReference.position.x, FloatPositionReference.position.y + Mathf.PingPong(Time.time, BobbingSpeed), FloatPositionReference.position.z);
					transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * DampSpeed);

					transform.RotateAround(FloatPositionReference.position, Vector3.up, RotationSpeed);
				}
			}
			else
			{
				Vector3 newPos = new Vector3(transform.position.x, originalPosition.y + Mathf.PingPong(Time.time, BobbingSpeed), transform.position.z);
				transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * DampSpeed);

				transform.RotateAround(transform.parent.position, Vector3.up, RotationSpeed);
			}
		}
	}

	public void ActivateFloatingObject()
	{
		isActivated = true;
	}
}
