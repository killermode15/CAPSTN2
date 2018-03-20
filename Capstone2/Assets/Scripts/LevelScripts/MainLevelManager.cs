using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour {

    public GameObject Player;
    public List<GameObject> Altars = new List<GameObject>();
    public GameObject Dialogue2;
	public GameObject Snake1;
	public List<GameObject> BaseUI = new List<GameObject>();
    public List<GameObject> SkillUI = new List<GameObject>();
    public GameObject SetDialogue1;
    public GameObject DoubleJumpPlatform;
    public GameObject ActivatedPlant;
    public GameObject Dialogue6;
    public GameObject OrbCounterUI;
    public List<GameObject> DoubleSpider;
    public GameObject Dialogue32;
    public GameObject PeaShooter1;
    public GameObject Dialogue42;
    private bool shit1, shit2, shit3, shit4;
    public GameObject ActivatedPlant1;
    public GameObject Dialogue7;
    public GameObject Dialogue43;
    public List<GameObject> TagTeam;
    public GameObject Checkpoint51;
    public GameObject VineSpawn3;

    // Use this for initialization
    void Start ()
    {
        shit1 = false;
        shit2 = false;
        shit3 = false;
        shit4 = false;
        //OrbCounter.SetActive(false);
        Dialogue2.SetActive(false);
        Dialogue6.SetActive(false);
        OrbCounterUI.SetActive(false);
        Dialogue32.SetActive(false);
        Dialogue42.SetActive(false);
        Dialogue7.SetActive(false);
        Dialogue43.SetActive(false);
        Checkpoint51.SetActive(false);

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
        //if (Altars.Count > 1)
        {
			if (Altars[0].GetComponent<AltarObject>().isActivated)
			{
				SkillUI[1].SetActive(true);
				Player.GetComponent<UseSkill>().GetElement(typeof(WindElement)).IsElementUnlocked = true;
				if (DoubleJumpPlatform.transform.position.y < 26.04f)
					DoubleJumpPlatform.transform.Translate(Vector3.up * 30.0f * Time.deltaTime, Space.World);
			}
		}
		//if (Altars.Count > 2)
		{
			if (Altars[1].GetComponent<AltarObject>().isActivated)
			{
				SkillUI[2].SetActive(true);
				Player.GetComponent<UseSkill>().GetElement(typeof(WaterElement)).IsElementUnlocked = true;
			}
		}
		//if (Altars.Count > 3)
		{
			if (Altars[2].GetComponent<AltarObject>().isActivated)
			{
                Debug.Log("Earth activateD");
				SkillUI[3].SetActive(true);
				Player.GetComponent<UseSkill>().GetElement(typeof(EarthElement)).IsElementUnlocked = true;
			}
		}
        //Check if Snake is dead to spawn dialogue trigger
        if (Snake1 != null)
        {
            if (Snake1.GetComponent<AIManager>().HP <= 0)
            {
                Dialogue2.SetActive(true);
                OrbCounterUI.SetActive(true);
            }
        }
		if (SetDialogue1.GetComponent<DialogueTrigger>().triggered)
		{
            SkillUI[0].SetActive(true);
            for (int i = 0; i < BaseUI.Count; i++)
			{
				BaseUI[i].SetActive(true);
			}
		}

        if (ActivatedPlant.GetComponent<Plant>().IsActivated)
        {
            Dialogue6.SetActive(true);
        }

        if(DoubleSpider[0] != null) {
            if (DoubleSpider[0].GetComponent<StateManager>().HP <= 0)
            {
                shit1 = true;
            }
        }
        if (DoubleSpider[1] != null)
        {
            if (DoubleSpider[1].GetComponent<StateManager>().HP <= 0)
            {
                shit2 = true;
            }
        }
        if (shit1 && shit2)
        {
            Dialogue32.SetActive(true);
            StartCoroutine(Wait());
        }

        if (PeaShooter1 != null)
        {
            if (PeaShooter1.GetComponent<StateManager>().HP <= 0 && !VineSpawn3.GetComponent<Plant>().IsActivated)
            {
                Dialogue42.SetActive(true);
            }
        }

        if(ActivatedPlant1.GetComponent<Plant>().IsActivated)
            Dialogue7.SetActive(true);

        if (TagTeam[0] != null)
        {
            if (TagTeam[0].GetComponent<StateManager>().HP <= 0)
            {
                shit3 = true;
            }
        }
        if (TagTeam[1] != null)
        {
            if (TagTeam[1].GetComponent<StateManager>().HP <= 0)
            {
                shit4 = true;
            }
        }
        if (shit3 && shit4)
        {
            Dialogue43.SetActive(true);
            Checkpoint51.SetActive(true);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
