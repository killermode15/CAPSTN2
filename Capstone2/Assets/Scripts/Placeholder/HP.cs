using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{

	public float Health;
	public float MaxHealth;
	public Slider HealthBar;
	public AudioClip DamageSFX;

	private float hpPercent;
	private float dampTime = 5f;
	private float currentLerpTime;

    [HideInInspector] public bool damagedByEnemy;

	public void Start()
	{
        damagedByEnemy = false;
        Health = MaxHealth;
		if (!HealthBar)
		{
			HealthBar = GameObject.FindGameObjectWithTag("HPBar").GetComponent<Slider>();
		}
	}

	public void Update()
	{
		if (Health <= 0) {
			Health = 0;
			GetComponent<PlayerController>().anim.SetBoolAnimParam("IsDead", true);
			GetComponent<PlayerController> ().CanMove = false;
			GetComponent<PlayerController> ().CanJump = false;
			GetComponent<PlayerController> ().enabled = false;
		}

		//HP Display
		hpPercent = Health / MaxHealth;
		if (HealthBar.value != hpPercent)
		{
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > dampTime)
			{
				currentLerpTime = dampTime;
			}

			HealthBar.value = Mathf.Lerp(HealthBar.value, hpPercent, currentLerpTime / dampTime);
		}
		else
			currentLerpTime = 0;
	}

	// Use this for initialization
	public void AddHealth(float val)
	{
		Health += val;
		if (Health > MaxHealth)
			Health = MaxHealth;
		//currentLerpTime = 0;
	}

	// Update is called once per frame
	public void RemoveHealth(float val)
	{
		Health -= val;

		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.clip = DamageSFX;
		source.GetComponent<AudioSource>().Play();
		Destroy(source, source.clip.length);


		if(val >= 5)
		{
			GetComponent<PlayerAnimation>().SetTriggerAnimParam("Hit");
		}
		//currentLerpTime = 0;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Projectile")) {
            damagedByEnemy = true;
			RemoveHealth (other.gameObject.GetComponent<Projectile> ().damage);
			Destroy (other.gameObject);
		}
		if (other.gameObject.CompareTag ("Enemy")) {
            damagedByEnemy = true;
            RemoveHealth (other.gameObject.GetComponent<StateManager>().collisionDamage);
		}
	}

	void OnControllerColliderHit(ControllerColliderHit collision){
		if (collision.gameObject.CompareTag ("DeathFall")) {
            RemoveHealth(Health);
        }
	}
}
