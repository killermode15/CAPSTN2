using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour {

    public GameObject Player;
    public List<GameObject> Altars = new List<GameObject>();
    public GameObject OrbCounter;
    public GameObject OrbCounterTrigger;

	// Use this for initialization
	void Start ()
    {
        OrbCounter.SetActive(false);

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
        if (OrbCounterTrigger.GetComponent<DialogueTrigger>().triggered)
        {
            OrbCounter.SetActive(true);
        }
	}
}
