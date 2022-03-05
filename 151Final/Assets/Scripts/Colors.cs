using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was copied from a video to be used a reference
public class Colors : AudioEvents
{
    
	public Color[] beatColors;
	public Color restColor;

	private int m_randomIndx;
	private SpriteRenderer m_img;
    [SerializeField] private Animator anim;

    private IEnumerator MoveToColor(Color _target)
	{
		Color _curr = m_img.color;
		Color _initial = _curr;
		float _timer = 0;
		
		while (_curr != _target)
		{
			_curr = Color.Lerp(_initial, _target, _timer / beatTime);
			_timer += Time.deltaTime;

			m_img.color = _curr;

			yield return null;
		}

		beat = false;
	}

	private Color RandomColor()
	{
		if (beatColors == null || beatColors.Length == 0) return Color.white;
		m_randomIndx = Random.Range(0, beatColors.Length);
		return beatColors[m_randomIndx];
	}

	public override void UpdateEffects()
	{
		base.UpdateEffects();

		if (beat) return;

		//m_img.color = Color.Lerp(m_img.color, restColor, smoothTime * Time.deltaTime);
	}

	public override void OnBeat()
	{
		base.OnBeat();

		Color _c = RandomColor();

		//StopCoroutine("MoveToColor");
        anim.ResetTrigger("PlayAnim");
		//StartCoroutine("MoveToColor", _c);
        anim.SetTrigger("PlayAnim");
	}

	private void Start()
	{
		m_img = GetComponent<SpriteRenderer>();
	}
}
