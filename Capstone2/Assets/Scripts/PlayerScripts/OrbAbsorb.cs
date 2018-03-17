using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbAbsorb : MonoBehaviour {

    public int OrbCount;
    public int MaxOrbs;
    public List<Image> OrbCounter = new List<Image>();

    // Use this for initialization
    void Start () {
        for (int i = 0; i < OrbCounter.Count; i++)
        {
            OrbCounter[i].enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

        //if (OrbCount <= MaxOrbs)
        //{
            if (OrbCount == 0)
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
            }
        //}
        //else
        //{
        //    for (int i = 0; i < MaxOrbs; i++)
        //    {
        //        OrbCounter[i].enabled = true;

        //    }
        //}
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
        }
    }
}
