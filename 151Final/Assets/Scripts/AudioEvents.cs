using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEvents : MonoBehaviour
{
    [SerializeField] private float triggerAmount;
    [SerializeField] private float timeForStep;
    public float beatTime;
    public float smoothTime;
    private float prevAudioVal, audioVal, timer;
    protected bool beat;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        UpdateEffects();
    }

    public virtual void UpdateEffects(){
        prevAudioVal = audioVal;
		audioVal = Spectrum.specVal;

		// if audio value went below the bias during this frame
		if (prevAudioVal > triggerAmount &&
			audioVal <= triggerAmount)
		{
			// if minimum beat interval is reached
			if (timer > timeForStep)
				OnBeat();
		}

		// if audio value went above the bias during this frame
		if (prevAudioVal <= triggerAmount &&
			audioVal > triggerAmount)
		{
			// if minimum beat interval is reached
			if (timer > timeForStep)
				OnBeat();
		}

		timer += Time.deltaTime;
    }

    public virtual void OnBeat(){
        //print("beat");
        timer = 0;
        beat = true;
    }
}
