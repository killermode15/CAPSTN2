using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour {

	public List<Toggle> GraphicSettings;
	public AudioMixer Mixer;
	public GameObject MenuObjectToFocus;

	private Toggle triggeredGraphicSetting;
	private Toggle lastActiveGraphicSetting;

	private void Start()
	{
		if(GraphicSettings.Count > 0)
			lastActiveGraphicSetting = GraphicSettings[0];
	}

	public void SetVolume(float val)
	{
		Mixer.SetFloat("MasterVolume", val);
	}

	public void SetFullscreen(bool val)
	{
		Screen.fullScreen = val;
	}

	public void SetQualitySettings(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void Update()
	{
		CloseMenu();
	}

	public void CloseMenu()
	{
		if(Input.GetButtonDown("Circle"))
		{
			MenuObjectToFocus.GetComponent<Selectable>().Select();
			gameObject.SetActive(false);
		}
	}


}
