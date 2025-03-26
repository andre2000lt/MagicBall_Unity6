using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GamePlayStatictic 
{
    private static string SHAKE_COUNT_KEY = "Shake_count1";

    public static void Init()
    {
        GamePlayEventBus.BallShaked_Event += OnBallShaked;

        ResetShakeCount();
    }


    // Shake
    public static int GetShakeCount()
    {
        return PlayerPrefs.GetInt(SHAKE_COUNT_KEY, 1);
    } 
    

    private static void IncreaseShakeCount()
    {
        int shakeCount = GetShakeCount() + 1;
        PlayerPrefs.SetInt(SHAKE_COUNT_KEY, shakeCount);
    }


    private static void ResetShakeCount()
    {
        PlayerPrefs.SetInt(SHAKE_COUNT_KEY, 0);
    }



    // Events
    private static void OnBallShaked()
    {
        IncreaseShakeCount();
        Debug.Log("shakes: " + GetShakeCount());
    }
}
