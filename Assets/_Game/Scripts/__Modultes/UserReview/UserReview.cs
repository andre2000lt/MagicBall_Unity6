using System.Collections;
using Google.Play.Review;
using UnityEngine;
using UnityEngine.UI;

public class UserReview : MonoBehaviour
{
    private const string STORAGE_KEY = "gameReview";

    [SerializeField] Button _rateGameButton;

    private bool IsUserRateGame 
    {
        get
        {
            int b = PlayerPrefs.GetInt(STORAGE_KEY, 0);
            return b != 0;
        }
    }

    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;



    void Start()
    {
        if (IsUserRateGame) return;

        _reviewManager = new ReviewManager();

        _rateGameButton.gameObject.SetActive(true);
        _rateGameButton.onClick.AddListener(ShowReviewDialog);
    }


    IEnumerator RequestReview()
    {
        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();


        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }

        PlayerPrefs.SetInt(STORAGE_KEY, 1);
        _rateGameButton.gameObject.SetActive(false);
    }


    private void ShowReviewDialog()
    {
        StartCoroutine(RequestReview());
    }
}
