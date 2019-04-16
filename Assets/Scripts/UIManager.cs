using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Material deformationMaterial = null;

    [SerializeField]
    private bool useSamplesFromAudio = false;

    private SpectrumManager spectrumManager = null;

    [SerializeField]
    private float sampleMultipler = 100.0f;

    [SerializeField]
    private float delayBy = 1.0f;
    private float elapsedTime = 0;

    private float audioClampValue;

    void Start() 
    {
        spectrumManager = SpectrumManager.Instance;
    }
    
    void Update()
    {
        if(deformationMaterial != null)
        {
            if(useSamplesFromAudio)
            {
                float value = spectrumManager.Samples[0] * sampleMultipler;
                audioClampValue = Mathf.Clamp(value, 0.1f, 100.0f);

                if(elapsedTime >= delayBy)
                {
                    noiseScaleChanged(audioClampValue);
                    elapsedTime = 0;
                }
                else {
                    elapsedTime += Time.deltaTime * 1.0f;
                    Debug.Log(elapsedTime);
                }
            }
        }
    }

    void timeSpeedChanged(float newValue) 
    {
        deformationMaterial.SetFloat("_TimeSpeed", audioClampValue);
    }

     void noiseScaleChanged(float newValue) 
     {
        deformationMaterial.SetFloat("_NoiseScale", audioClampValue);
    }
}
