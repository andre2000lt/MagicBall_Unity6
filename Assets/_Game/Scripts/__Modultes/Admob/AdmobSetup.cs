
using GoogleMobileAds.Api;
using UnityEngine;

public class AdmobSetup : MonoBehaviour
{
    public static AdmobSetup Instance;

    private InterstitialAd interstitialAd;
    //private string _interAdId = "ca-app-pub-3940256099942544/1033173712"; // test
    private string _interAdId = "ca-app-pub-3156689890203578/1321116376";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        MobileAds.Initialize(InitializationStatus => 
        {
            AdmobSetup.Instance.LoadInterstitialAd();
        });
        
    }
    public void OpenInspector()
    {
        MobileAds.OpenAdInspector(error => {
            // Error will be set if there was an issue and the inspector was not displayed.
        });
    }

    #region Interstitial
    public void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }


        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(_interAdId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    GamePlayEventBus.InvokeAddSentErrorEvent(error.ToString(), AdErrorType.FailedToLoadAd);
                    return;
                }

                interstitialAd = ad;
                RegisterEventHandlers(interstitialAd);
            });
    }



    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
            //GamePlayEventBus.InvokeAddSentErrorEvent("Interstitial ad Test error.", AdErrorType.Test);
        }
        else
        {
            GamePlayEventBus.InvokeAddSentErrorEvent("Interstitial ad is not ready yet.", AdErrorType.NotReady);
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd();
        }
    }


    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            GamePlayEventBus.InvokeAddSentErrorEvent(error.ToString(), AdErrorType.FaledToOpenContent);

            LoadInterstitialAd();
            
        };
    }
    #endregion
}


public enum AdErrorType
{
    NotReady,
    FaledToOpenContent,
    FailedToLoadAd,
    Test
}
