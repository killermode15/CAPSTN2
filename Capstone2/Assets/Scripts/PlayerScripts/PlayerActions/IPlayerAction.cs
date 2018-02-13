using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
	Move,
	Attack,
	Defend
}

public interface IPlayerAction {
	void UseAction();
}
