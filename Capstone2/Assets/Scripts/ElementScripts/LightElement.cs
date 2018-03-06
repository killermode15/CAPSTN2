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

			GameObject projectile = Instantiate(LightProjectilePrefab, player.transform.position, Quaternion.identity);
			projectile.GetComponent<LightProjectile>().InitializeProjectile(target.transform);

		}
		return true;
	}
}
