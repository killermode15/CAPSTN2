using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EaseType
{
	Linear,

	QuadraticIn,
	QuadraticOut,
	QuadraticInOut,

	CubicIn,
	CubicOut,
	CubicInOut,

	QuarticIn,
	QuarticOut,
	QuarticInOut,

	QuinticIn,
	QuinticOut,
	QuinticInOut,

	SinusoidalIn,
	SinusoidalOut,
	SinusoidalInOut,

	ExponentialIn,
	ExponentialOut,
	ExponentialInOut,

	CircularIn,
	CircularOut,
	CircularInOut,

	ElasticIn,
	ElasticOut,
	ElasticInOut,

	BackIn,
	BackOut,
	BackInOut,

	BounceIn,
	BounceOut,
	BounceInOut
}

public class EaseParticleScript : MonoBehaviour
{
	public GameObject Target;
	public EaseType EaseType;
	public float Duration;
	public AnimationCurve EaseCurve;

	public bool IsDoneEasing
	{
		get
		{
			return currDuration / Duration == 1;
		}
	}

	private float currDuration;

	// Use this for initialization
	void Start()
	{
		if (!Target)
			Target = GameObject.FindGameObjectWithTag("AbsorbOrb");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.KeypadPlus))
			currDuration = 0;

		currDuration += Time.deltaTime;
		if (currDuration >= Duration || Vector3.Distance(transform.position, Target.transform.position) <= 0f)
			currDuration = Duration;

		StartCoroutine(Move());

		//Debug.Log("Current Duration: " + currDuration);
		//Debug.Log("Eased Duration  : " + UseEaseType(EaseType, currDuration / Duration));
		//Debug.Log("Normalized Duration: " + UseEaseType(EaseType, currDuration / Duration));
	}

	IEnumerator Move()
	{

		if (IsDoneEasing)
		{
			//ParticleSystem ps = GetComponent<ParticleSystem>();
			//if (!ps)
			//{
			//	throw new System.NullReferenceException("Script is not attached to a particle system object.");
			//}

			Destroy(gameObject);

		}
		else
		{

			float yPos = Mathf.Abs(Target.transform.position.y * EaseCurve.Evaluate(UseEaseType(EaseType, currDuration / Duration)));
			transform.position = Vector3.Lerp(transform.position, Target.transform.position, UseEaseType(EaseType, currDuration / Duration));
		}
		yield return new WaitForEndOfFrame();
	}

	float UseEaseType(EaseType type, float val)
	{
		switch (type)
		{
			case EaseType.Linear: return Easing.Linear(val);
			case EaseType.QuadraticIn: return Easing.Quadratic.In(val);
			case EaseType.CubicIn: return Easing.Cubic.In(val);
			case EaseType.QuarticIn: return Easing.Quartic.In(val);
			case EaseType.QuinticIn: return Easing.Quintic.In(val);
			case EaseType.SinusoidalIn: return Easing.Sinusoidal.In(val);
			case EaseType.ExponentialIn: return Easing.Exponential.In(val);
			case EaseType.CircularIn: return Easing.Circular.In(val);
			case EaseType.ElasticIn: return Easing.Elastic.In(val);
			case EaseType.BackIn: return Easing.Back.In(val);
			case EaseType.BounceIn: return Easing.Bounce.In(val);

			case EaseType.QuadraticOut: return Easing.Quadratic.Out(val);
			case EaseType.CubicOut: return Easing.Cubic.Out(val);
			case EaseType.QuarticOut: return Easing.Quartic.Out(val);
			case EaseType.QuinticOut: return Easing.Quintic.Out(val);
			case EaseType.SinusoidalOut: return Easing.Sinusoidal.Out(val);
			case EaseType.ExponentialOut: return Easing.Exponential.Out(val);
			case EaseType.CircularOut: return Easing.Circular.Out(val);
			case EaseType.ElasticOut: return Easing.Elastic.Out(val);
			case EaseType.BackOut: return Easing.Back.Out(val);
			case EaseType.BounceOut: return Easing.Bounce.Out(val);

			case EaseType.QuadraticInOut: return Easing.Quadratic.InOut(val);
			case EaseType.CubicInOut: return Easing.Cubic.InOut(val);
			case EaseType.QuarticInOut: return Easing.Quartic.InOut(val);
			case EaseType.QuinticInOut: return Easing.Quintic.InOut(val);
			case EaseType.SinusoidalInOut: return Easing.Sinusoidal.InOut(val);
			case EaseType.ExponentialInOut: return Easing.Exponential.InOut(val);
			case EaseType.CircularInOut: return Easing.Circular.InOut(val);
			case EaseType.ElasticInOut: return Easing.Elastic.InOut(val);
			case EaseType.BackInOut: return Easing.Back.InOut(val);
			case EaseType.BounceInOut: return Easing.Bounce.InOut(val);
			default: return 0;
		}
	}
}
