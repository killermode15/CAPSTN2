using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TransitionToSnapshot : MonoBehaviour {

	public float TransitionSpeed = 2.0f;
	public AudioMixerSnapshot SnapshotTransition;
	public AudioMixerSnapshot LastSnapshotBeforeTransition;

	private bool hasTransitionedToNewSnapshot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TransitionSnapshot()
	{
		if (!hasTransitionedToNewSnapshot)
		{
			SnapshotTransition.TransitionTo(TransitionSpeed);
			hasTransitionedToNewSnapshot = true;
		}
		else
		{
			LastSnapshotBeforeTransition.TransitionTo(TransitionSpeed);
			hasTransitionedToNewSnapshot = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			TransitionSnapshot();
		}
	}
}
