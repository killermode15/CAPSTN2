using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbAbsorb : MonoBehaviour {

    public int OrbCount;
    public int MaxOrbs;
    //public List<Image> OrbCounter = new List<Image>();
    public TextMeshProUGUI Counter;

	public AudioClip OrbPickupSFX;

    // Use this for initialization
    void Start () {
        /*for (int i = 0; i < OrbCounter.Count; i++)
        {
            OrbCounter[i].enabled = false;
        }*/
    }
	
	// Update is called once per frame
	void Update () {
        
        if (OrbCount <= MaxOrbs)
        {
            Counter.text = (OrbCount.ToString());
            /*if (OrbCount == 0)
            {
                //Debug.Log("disabled to 0");
                for (int i = 0; i < OrbCounter.Count; i++)
                {
                    OrbCounter[i].enabled = false;
                }
            }
            else
            {
                //Debug.Log("enabled");
                for (int i = 0; i < OrbCount; i++)
                {
                    OrbCounter[i].enabled = true;
                }   
            }*/

        }
    }

	public bool IsOrbCounterFull()
	{
		return OrbCount >= MaxOrbs;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AbsorbOrb"))
        {
            OrbCount++;
            Destroy(other.gameObject);

			AudioSource source = gameObject.AddComponent<AudioSource>();
			source.clip = OrbPickupSFX;
			source.Play();
			Destroy(source, source.clip.length);
        }
    }
}
