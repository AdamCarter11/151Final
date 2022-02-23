using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    /*      Variables
    *   audioSpectrumVar is what is actually storing the spectrum data
    *   specVal is grabbing one value and allowing other scripts to access it
    */
    private float[] audioSpectrumVar;
    //specVal is accessible in other scripts
    public static float specVal {get; private set;}


    void Start()
    {
        audioSpectrumVar = new float[128];
    }

    void Update()
    {
        AudioListener.GetSpectrumData(audioSpectrumVar, 0, FFTWindow.BlackmanHarris);
        if (audioSpectrumVar != null && audioSpectrumVar.Length > 0){
            //we can set it to zero cause it's in update and the audioSpectrumVar is constantly changing
            specVal = audioSpectrumVar[0] * 100;
        }
    }
}
