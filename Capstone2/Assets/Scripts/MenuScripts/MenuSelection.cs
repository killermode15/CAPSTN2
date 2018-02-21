using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour {

	public GameObject ButtonParent;
	public GameObject SettingsParent;
	public GameObject SettingsObjectToFocus;

	private List<Button> buttons;

	// Use this for initialization
	void Start () {
		buttons = new List<Button>();
		foreach(Transform button in ButtonParent.transform)
		{
			buttons.Add(button.GetComponent<Button>());
		}
		buttons[0].Select();
	}
	
	public void OpenSettings()
	{
		SettingsParent.SetActive(true);
		SettingsObjectToFocus.GetComponent<Selectable>().Select();
	}

	public void Exit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void StartGame(){
		SceneManager.LoadScene ("Tutorial Level");
	}

}
