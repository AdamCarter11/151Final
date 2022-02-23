using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was copied from a video to be used a reference
public class Scales : AudioEvents {

    public Vector3 beatScale;
	public Vector3 restScale;

	private IEnumerator MoveToScale(Vector3 _target)
	{
		Vector3 _curr = transform.localScale;
		Vector3 _initial = _curr;
		float _timer = 0;

		while (_curr != _target)
		{
			_curr = Vector3.Lerp(_initial, _target, _timer / beatTime);
			_timer += Time.deltaTime;

			transform.localScale = _curr;

			yield return null;
		}

		beat = false;
	}

	public override void UpdateEffects()
	{
		base.UpdateEffects();

		if (beat) return;

		transform.localScale = Vector3.Lerp(transform.localScale, restScale, smoothTime * Time.deltaTime);
	}

	public override void OnBeat()
	{
		base.OnBeat();
        
		StopCoroutine("MoveToScale");
		StartCoroutine("MoveToScale", beatScale);
	}
}
