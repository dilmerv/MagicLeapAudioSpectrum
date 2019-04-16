using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityLWRPEssentials.Assets.Scripts.Core;

public class SpectrumManager : MonoBehaviourSingleton<SpectrumManager>
{
    [SerializeField]
    private AudioSource audioSource = null;

    [SerializeField]
    private float[] samples = new float[512];

    private bool mute = false;

    public bool IsMuted  
    {
        get {
            return mute;
        }
        set {
            mute = value;
        }
    }

    public float[] Samples
    {
        get 
        {
            return this.samples;
        }
    }
    
    public void MuteOff()
    {
        IsMuted = false;
        audioSource.volume = 1;
    }

    public void MuteOn()
    {
        IsMuted = true;
        audioSource.volume = 0;
        samples[0] = 0;
    }

    void Update() 
    {
        if(!IsMuted)
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        }
    }
}