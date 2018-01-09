using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour {

	public string Name;

	public abstract void InteractWith();
}
