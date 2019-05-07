using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityLWRPEssentials.Assets.Scripts.Core;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField]
    private AudioSource[] audioSources = null;

    [SerializeField]
    private Text songText = null;

    [SerializeField]
    private AudioSource currentAudioSource = null;

    void Awake()
    {
        if(songText == null)
        {
            Debug.Log("You must set the songText through the inspector");
            enabled = false;
        }
        if(audioSources?.Length == 0)
        {
            Debug.Log("You haven't set any audio sources - disabling audio manager game object");
            enabled = false;
        }

        GetSongName();
    }

    void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.S))
        {
            ShuffleAudio();
            GetSongName();
        }
        #endif
    }

    public void ShuffleAudio()
    {
        int randomAudioSourceNumber = Random.Range(0, audioSources.Length);
        currentAudioSource = audioSources[randomAudioSourceNumber];
        SpectrumManager.Instance.SwapAudioSource(currentAudioSource);
        currentAudioSource.Play();
    }

    public void GetSongName() => songText.text = $"{currentAudioSource.clip.name.Replace("_", string.Empty)}";
}
