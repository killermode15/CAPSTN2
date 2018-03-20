using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject EndAltar;
    public GameObject EndDialogue;

    public List<GameObject> OrbParticles;
    public Color lerpedColor;

    public GameObject DeadTrees;
    public GameObject PrettyTrees;



    // Use this for initialization
    void Start()
    {
        EndDialogue.SetActive(false);
        PrettyTrees.SetActive(true);
        for (int i = 0; i < PrettyTrees.gameObject.transform.childCount; i++)
        {
            PrettyTrees.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < OrbParticles.Count; i++)
        {
            //lerpedColor = Color.Lerp(OrbParticles[i].GetComponent<ParticleSystem>().startColor;
            OrbParticles[i].GetComponent<ParticleSystem>().startColor = Color.Lerp(Color.white, Color.white, 4.0f * Time.deltaTime);
        }
        if (EndAltar.GetComponent<AltarObject>().isActivated)
        {
            EndDialogue.SetActive(true);
            StartCoroutine(PrettyTreeSpawn());
            StartCoroutine(DeadTreeSpawn());
        }
    }

    IEnumerator PrettyTreeSpawn()
    {
        for (int i = 0; i < PrettyTrees.gameObject.transform.childCount; i++)
        {
            PrettyTrees.gameObject.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator DeadTreeSpawn()
    {
        for (int i = 0; i < DeadTrees.gameObject.transform.childCount; i++)
        {
            DeadTrees.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            yield return new WaitForSeconds(0);
        }
    }
}
