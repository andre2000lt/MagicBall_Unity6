using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioSettings
{
    public static bool IsSoundsOn
    {
        get
        {
            if (PlayerPrefs.HasKey("Sounds"))
                return PlayerPrefs.GetInt("Sounds") == 1;

            PlayerPrefs.SetInt("Sounds", 1);
            return true;
        }
    }

    public static void ToggleSoundsState()
    {
        if (IsSoundsOn)
        {
            PlayerPrefs.SetInt("Sounds", 0);
            return;
        }

        PlayerPrefs.SetInt("Sounds", 1);
    }
}