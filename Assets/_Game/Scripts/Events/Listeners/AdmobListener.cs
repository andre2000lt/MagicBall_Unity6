using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobListener : MonoBehaviour
{

    public static void Init()
    {
        GamePlayEventBus.BallShaked_Event += OnBallShaked;
        GamePlayEventBus.BallSaved_Event += OnBallSaved;
    }

    private static void OnBallSaved()
    {
        //AdmobSetup.Instance.ShowInterstitialAd();
    }


    // Events
    private static void OnBallShaked()
    {
        if (PlayerPrefs.GetInt("noAds") == 1) return;

        int shakeCount = GamePlayStatictic.GetShakeCount();
        if (shakeCount % 4 == 0  && shakeCount != 0)
        {
            AdmobSetup.Instance.StartCoroutine(ShowAdAsync());
        }      
    }


    private static IEnumerator ShowAdAsync()
    {
        Debug.Log("Show!!!");
        yield return new WaitForSeconds(2);
        AdmobSetup.Instance.ShowInterstitialAd();
    }
}
