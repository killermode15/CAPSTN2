using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbIndicatorUI : MonoBehaviour {

	public Image Element;
	Color elementColor;
	float elementAlpha;

	public Image Enemy;
	Color enemyColor;
	float enemyAlpha;

	// Use this for initialization
	void Start () {
		elementColor = Element.GetComponent<Image> ().color;
		enemyColor = Enemy.GetComponent<Image> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		elementColor.a = elementAlpha;
		Element.GetComponent<Image> ().color = elementColor;

		enemyColor.a = enemyAlpha;
		Enemy.GetComponent<Image> ().color = enemyColor;

		if (GetComponent<Absorb> ().CurrentMode == Absorb.AbsorbMode.Corruption) {
			elementAlpha = 0.1f;
			enemyAlpha = 1.0f;
		} 
		if (GetComponent<Absorb> ().CurrentMode == Absorb.AbsorbMode.Element) {
			enemyAlpha = 0.1f;
			elementAlpha = 1.0f;
		}
	}
}
