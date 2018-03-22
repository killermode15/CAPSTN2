using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Elements/Light Element")]
public class LightElement : Element
{
	public GameObject LightProjectilePrefab;

	public override bool Use(GameObject player)
	{
		if (!base.Use(player))
			return false;

		if (IsBaseUseable())
		{
			GameObject target = player.GetComponent<LockOn>().currentTarget;

			if (!target)
				return false;

			GameObject projectile = Instantiate(LightProjectilePrefab, 
				new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);
			projectile.GetComponent<LightProjectile>().InitializeProjectile(target.transform);

			AudioSource source = player.AddComponent<AudioSource>();
			source.clip = SoundEffect;
			source.Play();
			Destroy(source, source.clip.length);

		}
		return true;
	}
}
