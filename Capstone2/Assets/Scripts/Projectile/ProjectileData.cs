using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
public class ProjectileData : ScriptableObject {

	public float ProjectileRange;
	public float ProjectileSpeed;
	public float ProjectileDamage;
	public float ProjectileLife;
}
