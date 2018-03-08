using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class LightProjectile : MonoBehaviour {

	public ProjectileData ProjectileData;
	
	public Vector3 Direction { get; set; }

	private Transform target;
	private bool initialized;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
			initialized = true;
		if(initialized)
		{
			//transform.Translate(Direction * ProjectileData.ProjectileSpeed * Time.deltaTime);
			transform.position += Direction * Time.deltaTime * ProjectileData.ProjectileSpeed;
			StartCoroutine(DestroyProjectile(ProjectileData.ProjectileLife));
			//transform.position += Vector3.Lerp(transform.position, transform.position + (Direction * ProjectileData.ProjectileRange), Time.deltaTime * ProjectileData.ProjectileSpeed);
		}
	}

	public void InitializeProjectile(Transform target)
	{
		if (!target || initialized)
			return;
		this.target = target;
		initialized = true;
		Direction = target.position - transform.position;
		Direction.Normalize();
	}

	IEnumerator DestroyProjectile(float lifeTime)
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(gameObject);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Ground"))
		{
			Destroy(gameObject);
		}
		if(other.CompareTag("Enemy"))
		{
			other.GetComponent<StateManager>().GetDamage();
			Destroy(gameObject);
			//Do damage here
		}
	}
}
