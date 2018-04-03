using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarObject : MonoBehaviour
{
	public bool isActivated;
	public List<FloatingObject> FloatingObjects;
	public List<ParticleSystem> ParticleFX;
    public GameObject WorldTreeOrbParticlePrefab;
    public string ParticlePathName;

	public AudioClip ActivationSFX;

	private GameObject player;
	private bool hasTriggeredEffects;

	// Use this for initialization
	void Start()
	{
		isActivated = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (isActivated)
		{
			if (!hasTriggeredEffects)
			{
				hasTriggeredEffects = true;
				ActivateFloatingObjects();
				ActivateParticleEffects();
				transform.GetChild(0).GetComponent<DialogueTrigger>().TriggerDialogue();
				if (player)
					player.GetComponent<OrbAbsorb>().OrbCount = 0;

                Vector3 stonePosition = transform.GetChild(2).GetChild(1).position;
                GameObject ps = Instantiate(WorldTreeOrbParticlePrefab, GameObject.Find("WorldTree").transform);
                ps.transform.position = stonePosition;
                ps.GetComponent<ParticleFollowPath>().PathName = ParticlePathName;
                ps.GetComponent<ParticleFollowPath>().Activate();

				AudioSource source = gameObject.AddComponent<AudioSource>();
				source.clip = ActivationSFX;
				source.Play();
				Destroy(source, source.clip.length);
			}
		}

		if (!player)
			return;


        if (player.GetComponent<OrbAbsorb>().IsOrbCounterFull() && !isActivated)
        {
            if (InputManager.Instance.GetKey(ControllerInput.ActivateAltar))
            {
                isActivated = true;
            }
        }
        else if (!player.GetComponent<OrbAbsorb>().IsOrbCounterFull() && !isActivated)
        {
            if (InputManager.Instance.GetKeyDown(ControllerInput.ActivateAltar))
            {
                transform.GetChild(1).GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
	}

	void ActivateFloatingObjects()
	{
		foreach (FloatingObject obj in FloatingObjects)
		{
			obj.ActivateFloatingObject();
		}
	}

	void ActivateParticleEffects()
	{
		foreach (ParticleSystem ps in ParticleFX)
		{
			ps.Play();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			player = other.gameObject;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			player = null;
		}
	}
}
