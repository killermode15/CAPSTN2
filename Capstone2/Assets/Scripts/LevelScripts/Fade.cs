using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	public Image image;
	Color color;
	public float alphaValue;
	float Timer;
	public float fadeSpeed;
	public bool startFade;

	// Use this for initialization
	void Start () {
		Timer = 0.0f;
		alphaValue = 0.0f;
		if(!image)
			image = GetComponent<Image> ();
		color = image.color;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (startFade)
			Timer += fadeSpeed * Time.deltaTime;
		else if (!startFade)
			Timer = 0;
		alphaValue = Timer;
		color.a = alphaValue;
		image.color = color;*/

		alphaValue = Timer;
		color.a = alphaValue;
		image.color = color;

		if (alphaValue >= 0.75f) {
			Timer = 0;
		}
	}

	void FadeOut(){

	}

	public IEnumerator Fading(){
		/*Timer += fadeSpeed * Time.deltaTime;
		alphaValue = Timer;
		color.a = alphaValue;
		image.color = color;*/
		Timer += fadeSpeed * Time.deltaTime;


		/*GetComponent<Level1Manager>().shit1 = false;
		GetComponent<Level1Manager>().shit2 = false;*/

		yield return null;
	}
}
