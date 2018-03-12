using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour {

    public GameObject Player;
    public List<GameObject> Altars = new List<GameObject>();
    public GameObject OrbCounter;
    public GameObject OrbCounterTrigger;
	//public GameObject Snake1;
	public List<GameObject> BaseUI = new List<GameObject>();
    public List<GameObject> SkillUI = new List<GameObject>();
    public GameObject SetDialogue1;
    public GameObject DoubleJumpPlatform;

	// Use this for initialization
	void Start ()
    {
        OrbCounter.SetActive(false);
		OrbCounterTrigger.SetActive(true); //dapat false
		for (int i = 0; i < BaseUI.Count; i++)
		{
			BaseUI[i].SetActive(false);
		}
        for (int i = 0; i < SkillUI.Count; i++)
        {
            SkillUI[i].SetActive(false);
        }

        Player.GetComponent<UseSkill>().GetElement(typeof(WindElement)).IsElementUnlocked = false;
        Player.GetComponent<UseSkill>().GetElement(typeof(WaterElement)).IsElementUnlocked = false;
        Player.GetComponent<UseSkill>().GetElement(typeof(EarthElement)).IsElementUnlocked = false;
    }
	
	// Update is called once per frame
	void Update () {
		//if first Altar activates the Wind Element
		if (Altars.Count > 1)
		{
			if (Altars[0].GetComponent<AltarObject>().isActivated)
			{
				SkillUI[1].SetActive(true);
				Player.GetComponent<UseSkill>().GetElement(typeof(WindElement)).IsElementUnlocked = true;
				if (DoubleJumpPlatform.transform.position.y < 26.04f)
					DoubleJumpPlatform.transform.Translate(Vector3.up * 30.0f * Time.deltaTime, Space.World);
			}
		}
		if (Altars.Count > 2)
		{
			if (Altars[1].GetComponent<AltarObject>().isActivated)
			{
				SkillUI[2].SetActive(true);
				Player.GetComponent<UseSkill>().GetElement(typeof(WaterElement)).IsElementUnlocked = true;
			}
		}
		if (Altars.Count > 3)
		{
			if (Altars[2].GetComponent<AltarObject>().isActivated)
			{
				SkillUI[3].SetActive(true);
				Player.GetComponent<UseSkill>().GetElement(typeof(EarthElement)).IsElementUnlocked = true;
			}
		}
        //Check if Snake is dead to spawn dialogue trigger
        //if (Snake1.GetComponent<Dead>().isActiveAndEnabled)
        //{
        OrbCounterTrigger.SetActive(true);
			if (OrbCounterTrigger.GetComponent<DialogueTrigger>().triggered)
			{
				OrbCounter.SetActive(true);
			}
		//}
		if (SetDialogue1.GetComponent<DialogueTrigger>().triggered)
		{
            SkillUI[0].SetActive(true);
            for (int i = 0; i < BaseUI.Count; i++)
			{
				BaseUI[i].SetActive(true);
			}
		}
	}
}
