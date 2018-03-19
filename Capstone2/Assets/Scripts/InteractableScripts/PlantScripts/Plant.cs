using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Transform VineBridge;
    private Transform PlantShoot;

    public bool IsActivated
    {
        get { return isActivated; }
    }

    [HideInInspector] public bool isActivated;
    private bool hasSpawnedPlatform;


    // Use this for initialization
    void Start()
    {
        VineBridge = this.gameObject.transform.GetChild(0);
        PlantShoot = this.gameObject.transform.GetChild(1);
        VineBridge.gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
            return;

        if (!hasSpawnedPlatform)
        {
            //Spawn the plant here
            PlantShoot.gameObject.SetActive(false);
            VineBridge.gameObject.SetActive(true);
            List<VineBridgeDissolver> bridgeScripts = VineBridge.GetComponents<VineBridgeDissolver>().ToList();

            foreach (VineBridgeDissolver script in bridgeScripts)
            {
                script.ActivateDissolver();
            }
            ActivatePlantCollider scriptReference = GetComponent<ActivatePlantCollider>();
            if (scriptReference)
                GetComponent<ActivatePlantCollider>().ActivateColliders();
            hasSpawnedPlatform = true;
            Debug.Log("Im hit with water");
        }

    }

    public void ActivatePlant()
    {
        isActivated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }
}
