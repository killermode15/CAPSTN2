using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{

    public GameObject EndAltar;
    //public GameObject EndDialogue;

    public List<GameObject> OrbParticles;
    public Color lerpedColor;

    public GameObject DeadTrees;
    public GameObject PrettyTrees;

    public List<GameObject> disabledUI;
    public Material skybox;
    [HideInInspector] public Color initialColor = new Color(52, 9, 90, 128);
    private Color changeColor;
    private bool hasStartedSkyboxColor;

    public GameObject BadOrb;
    public GameObject GoodOrb;
    public Light orbPointLight;

    // Use this for initialization
    void Start()
    {
        GoodOrb.SetActive(false);
        //EndDialogue.SetActive(false);
        PrettyTrees.SetActive(true);
        for (int i = 0; i < PrettyTrees.gameObject.transform.childCount; i++)
        {
            PrettyTrees.gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

        changeColor = new Color(44, 135, 212);
    }

    // Update is called once per frame
    void Update()
    {
        ///for the orb being white
        /*for (int i = 0; i < OrbParticles.Count; i++)
        {
            //lerpedColor = Color.Lerp(OrbParticles[i].GetComponent<ParticleSystem>().startColor;
            OrbParticles[i].GetComponent<ParticleSystem>().startColor = Color.Lerp(Color.white, Color.white, 4.0f * Time.deltaTime);
        }*/

        ///ending altar is activated, ending begins
        if (EndAltar.GetComponent<AltarObject>().isActivated)
        {
            for (int i = 0; i < disabledUI.Count; i++)
            {
                disabledUI[i].SetActive(false);
            }

            orbPointLight.GetComponent<Light>().color = new Color(134, 169, 178) / 255; //initial 114, 0 ,255
            //skybox.SetColor("_Tint", changeColor/225);
            if (!hasStartedSkyboxColor)
            {
                StartCoroutine(ColorLerp(2));
                hasStartedSkyboxColor = true;
            }
            //EndDialogue.SetActive(true);
            StartCoroutine(PrettyTreeSpawn());
            StartCoroutine(DeadTreeSpawn());

            BadOrb.transform.localScale = Vector3.Lerp(BadOrb.transform.localScale, Vector3.zero, 1.0f * Time.deltaTime);
            GoodOrb.SetActive(true);
            GoodOrb.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(3, 1.5f, 3), 2.0f * Time.deltaTime);
            if (BadOrb.transform.localScale == Vector3.zero){
                BadOrb.SetActive(false);
            }
        }
    }

    IEnumerator ColorLerp(float duration)
    {
        float currentDuration = 0;
        float perc = 0;

        while (perc < 1)
        {
            currentDuration += Time.deltaTime;
            skybox.SetColor("_Tint", Color.Lerp(initialColor/255, changeColor / 255, perc));

            perc = currentDuration / duration;
            Debug.Log(perc);
            yield return new WaitForEndOfFrame();
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
