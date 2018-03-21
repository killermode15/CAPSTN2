using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour {

	public List<AudioClip> SoundEffects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Play () {
		foreach(AudioClip soundEffect in SoundEffects)
		{

		}
	}
}
