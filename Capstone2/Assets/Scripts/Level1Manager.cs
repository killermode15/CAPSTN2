using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour {

	public GameObject CorruptedLevel;
	public GameObject  CleanLevel;
	public bool isCorrupted;

	// Use this for initialization
	void Start () {
		isCorrupted = true;
	}
	
	// Update is called once per frame
	void Update () {
		CorruptedLevel.SetActive(isCorrupted);
		CleanLevel.SetActive(!isCorrupted);
	}
}
