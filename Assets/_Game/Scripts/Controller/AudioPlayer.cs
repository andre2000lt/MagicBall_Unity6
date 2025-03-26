using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    public static Dictionary<SoundClipName, string> SoundResourcesByName = new Dictionary<SoundClipName, string>()
    {
        {SoundClipName.Answer, "Sounds/Answer"}
    };


    public AudioSource SoundPlayer;
    public AudioSource MusicPlayer;


    private AudioClip _currentSoundClip;


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SoundPlayer = GameObject.Find("Audio").GetComponent<AudioSource>();
    }


    public void PlaySound(SoundClipName soundClipName)
    {
        if (AudioSettings.IsSoundsOn == false)
            return;

        _currentSoundClip = Resources.Load<AudioClip>(SoundResourcesByName[soundClipName]);
        Instance.SoundPlayer.clip = _currentSoundClip;
        Instance.SoundPlayer.Play();
    }
}


public enum SoundClipName
{
    Answer,
}
