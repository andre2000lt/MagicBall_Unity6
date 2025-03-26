using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioSettingsUI : MonoBehaviour
{
    private Toggle _soundToggle;



    private void Awake()
    {
        _soundToggle = GameObject.Find("SoundsToggle").GetComponent<Toggle>();

        _soundToggle.onValueChanged.AddListener(delegate
        {
            OnSoundsToggleValueChange(_soundToggle);
        });
    }



    private void OnEnable()
    {
        _soundToggle.SetIsOnWithoutNotify(AudioSettings.IsSoundsOn);
    }


    public static void OnSoundsToggleValueChange(Toggle soundToggle)
    {
        AudioSettings.ToggleSoundsState();
    }
}
