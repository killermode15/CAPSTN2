using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PauseManager : MonoBehaviour {

	private static PauseManager instance;
	public static PauseManager Instance
	{
		get
		{
			if(!instance)
			{
				instance = GameObject.FindObjectOfType<PauseManager>();
				if(!instance)
				{
					GameObject newInstance = new GameObject("Pause Manager");
					instance = newInstance.AddComponent<PauseManager>();
				}
			}
			return instance;
		}
	}

	public bool IsPaused;
	public delegate void OnPause ();
	public delegate void OnUnPause();
	public OnPause onPause;
	public OnUnPause onUnPause;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	public void Pause(){
		IsPaused = true;
		if (onPause != null) {
			onPause.Invoke ();
		}
	}

	public void UnPause(){
		IsPaused = false;
		if (onUnPause != null) {
			onUnPause.Invoke ();
		}
	}

	public void addPausable(IPausable pausable){
			onPause += pausable.Pause;
			onUnPause += pausable.UnPause;

	}

	public void removePausable(IPausable unPausable){
			onPause -= unPausable.Pause;
			onUnPause -= unPausable.UnPause;

	}
}
