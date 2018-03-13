using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBridgeDissolver : MonoBehaviour {

	public Material DissolveMaterial;
	public Transform ReferenceStartTransform;
	public Vector3 DesiredDissolvePosition;
	public Vector3 DissolvePositionOffset;

	public float DissolveRate;
	public bool UseReference;
	public float DissolveDuration;

	private bool IsActivated = false;
	private Vector3 input = Vector3.zero;

	private void OnValidate()
	{
		Vector3 dissolvePos = Vector3.zero;

		if (UseReference)
		{
			dissolvePos = ReferenceStartTransform.position;
		}
		else
		{
			dissolvePos = transform.position;
		}

		dissolvePos += DissolvePositionOffset;

		Initialize(dissolvePos);
	}

	private void Initialize(Vector3 dissolvePos)
	{
		DissolveMaterial.SetVector("_DissolvePosition", Vector3.zero);
		DissolveMaterial.SetVector("_DissolveStartPosition", dissolvePos);
	}

	// Use this for initialization
	void Start () {

		Vector3 dissolvePos = Vector3.zero;

		if (UseReference)
		{
			if (ReferenceStartTransform)
			{
				dissolvePos = ReferenceStartTransform.position;
			}
		}
		else
		{
			dissolvePos = transform.position;
		}

		dissolvePos += DissolvePositionOffset;

		Initialize(dissolvePos);
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Space))
			ActivateDissolver();
#endif
	}

	public void ActivateDissolver()
	{
		if (!IsActivated)
		{
			StartCoroutine(Dissolve());
			IsActivated = true;
		}
	}

	IEnumerator Dissolve()
	{
		Vector3 currentDissolvePosition = Vector3.zero;
		float timer = 0;

		while (timer < DissolveDuration)
		{
			timer += Time.deltaTime * DissolveRate;
			float perc = timer / DissolveDuration;
			currentDissolvePosition = Vector3.Lerp(currentDissolvePosition, DesiredDissolvePosition, perc);
			DissolveMaterial.SetVector("_DissolvePosition", currentDissolvePosition);
			yield return new WaitForEndOfFrame();
		}
	}
}
