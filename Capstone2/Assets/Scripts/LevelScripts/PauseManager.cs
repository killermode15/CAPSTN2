using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PauseManager : MonoBehaviour {

	public static PauseManager Instance;

	public delegate void OnPause ();
	public delegate void OnUnPause();
	public OnPause onPause;
	public OnUnPause onUnPause;

	void Awake(){
		DontDestroyOnLoad (gameObject);
		Instance = this;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Update () {
		
	}

	public void Pause(){
		if (onPause != null) {
			Debug.Log ("pause");
			onPause.Invoke ();
		}
	}

	public void UnPause(){
		if (onUnPause != null) {
			Debug.Log ("unpaused");
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
