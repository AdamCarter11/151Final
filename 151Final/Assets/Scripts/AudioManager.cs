 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip defaultSong;
    public static AudioManager instance;
    private AudioSource track1, track2;
    private bool isPlayingTrack;

    private void Awake() {
        if(instance == null)
            instance = this;
    }
    private void Start() {
        track1 = gameObject.AddComponent<AudioSource>();
        track1.loop = true;
        track2 = gameObject.AddComponent<AudioSource>();
        track2.loop = true;
        isPlayingTrack = true;

        SwapTrack(defaultSong);
    }

    public void SwapTrack(AudioClip newClip){
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));
        isPlayingTrack = !isPlayingTrack;
    }

    IEnumerator FadeTrack(AudioClip newClip){
        float timeToFade = 1f;
        float timeElapsed = 0;
        
        if(isPlayingTrack){
            track2.clip = newClip;
            track2.Play();
            while(timeElapsed < timeToFade){
                track2.volume = Mathf.Lerp(0,.5f,timeElapsed/timeToFade);
                track1.volume = Mathf.Lerp(.5f,0,timeElapsed/timeToFade);
                timeElapsed+= Time.deltaTime;

                yield return null;
            }
            track1.Stop();
        }
        else{
            track1.clip = newClip;
            track1.Play();
            while(timeElapsed < timeToFade){
                track1.volume = Mathf.Lerp(0,1,timeElapsed/timeToFade);
                track2.volume = Mathf.Lerp(1,0,timeElapsed/timeToFade);
                timeElapsed+= Time.deltaTime;
                
                yield return null;
            }
            track2.Stop();
        }
    }
}
