using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour {

    public GameObject Player;
    public List<GameObject> Altars = new List<GameObject>();
    public GameObject OrbCounter;
    public GameObject OrbCounterTrigger;
	public GameObject Snake0;
	public List<GameObject> BaseUI = new List<GameObject>();
	public GameObject SetDialogue1;

	// Use this for initialization
	void Start ()
    {
        OrbCounter.SetActive(false);
		OrbCounterTrigger.SetActive(true); //dapat false
		for (int i = 0; i < BaseUI.Count; i++)
		{
			BaseUI[i].SetActive(false);
		}


		Player.GetComponent<UseSkill>().GetElement(typeof(WindElement)).IsElementUnlocked = false;
        Player.GetComponent<UseSkill>().GetElement(typeof(WaterElement)).IsElementUnlocked = false;
        Player.GetComponent<UseSkill>().GetElement(typeof(EarthElement)).IsElementUnlocked = false;
    }
	
	// Update is called once per frame
	void Update () {
        //if first Altar activates the Wind Element
        if (Altars[0].GetComponent<AltarObject>().isActivated)
        {
            Player.GetComponent<UseSkill>().GetElement(typeof(WindElement)).IsElementUnlocked = true;
        }
        if (Altars[1].GetComponent<AltarObject>().isActivated)
        {
            Player.GetComponent<UseSkill>().GetElement(typeof(WaterElement)).IsElementUnlocked = true;
        }
		//if (Snake0.GetComponent<Dead>().isActiveAndEnabled)
		//{
			OrbCounterTrigger.SetActive(true);
			if (OrbCounterTrigger.GetComponent<DialogueTrigger>().triggered)
			{
				OrbCounter.SetActive(true);
			}
		//}
		if (SetDialogue1.GetComponent<DialogueTrigger>().triggered)
		{
			for (int i = 0; i < BaseUI.Count; i++)
			{
				BaseUI[i].SetActive(true);
			}
		}
	}
}
