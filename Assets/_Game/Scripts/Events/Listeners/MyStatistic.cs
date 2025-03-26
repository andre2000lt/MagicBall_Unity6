using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStatistic : MonoBehaviour
{
    private const string BALL_SHAKE_COUNT_URL = "https://pcgame.lt/magic/stats/ball_shake_count.php";
    private const string USER_SHAKE_URL = "https://pcgame.lt/magic/stats/user_shake.php";
    private const string USERS_URL = "https://pcgame.lt/magic/stats/magic_users.php";
    private const string AD_ERROR_URL = "https://pcgame.lt/magic/stats/ad_errors.php";

    public static MyStatistic Instance;

    private string _deviceId;
    private string _language;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


        _deviceId = SystemInfo.deviceUniqueIdentifier;
        _language = Application.systemLanguage.ToString();

        GamePlayEventBus.BallShaked_Event += SendBallToDB;
        GamePlayEventBus.BallShaked_Event += SendUserShakeToDB;
        GamePlayEventBus.GameStarted_Event += SendUserToDB;
        GamePlayEventBus.AddSentError_Event += SendAdErrorToDB;
    }

    private void SendAdErrorToDB(string errorMessage, AdErrorType errorType)
    {
        Dictionary<string, string> formData = new Dictionary<string, string>();
        formData.Add("errorMessage", errorMessage);
        formData.Add("errorType", errorType.ToString());
        formData.Add("language", _language);
        HttpRequest.UploadFormToServer(formData, AD_ERROR_URL);
    }


    private void SendUserShakeToDB()
    {
        Dictionary<string, string> formData = new Dictionary<string, string>();
        formData.Add("deviceId", _deviceId);
        HttpRequest.UploadFormToServer(formData, USER_SHAKE_URL);
    }


    private void SendUserToDB()
    {
        Dictionary<string, string> formData = new Dictionary<string, string>();
        formData.Add("deviceId", _deviceId);
        formData.Add("language", _language);

        HttpRequest.UploadFormToServer(formData, USERS_URL);

        Debug.Log("MainScene Loaded");
    }

    private void SendBallToDB()
    {
        var currentBall = BallsStorage.GetCurrentBall();
        var currentBallName = currentBall.Name;
        string currenBallType = currentBall.BallType.ToString();

        Dictionary<string, string> formData = new Dictionary<string, string>();
        formData.Add("ballName", currentBallName);
        formData.Add("ballType", currenBallType);

        HttpRequest.UploadFormToServer(formData, BALL_SHAKE_COUNT_URL);
    }
}
