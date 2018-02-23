using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Stats", menuName = "AI/New Stats")]
public class BossStats : ScriptableObject {

	public float WaitDuration;
	public float MoveSpeed;
}
