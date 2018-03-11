using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrbAbsorb : MonoBehaviour {

    public int OrbCount;
    public int MaxOrbs;
    public TextMeshProUGUI Counter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Counter.text = ("OrbCounter: " + OrbCount + "/" + MaxOrbs);
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
