using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class MainUI : MonoBehaviour
{
    [Header("Dropdown")]
    [SerializeField] private Dropdown _ballsDropdown;

    [Header("Top Buttons")]
    [SerializeField] private Button _addBallButton;
    [SerializeField] private Button _editBallButton;
    [SerializeField] private Button _removeBallButton;

    [Header("Bottom Buttons")]
    [SerializeField] private Button _shakeButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _noAdsButton;

    [Header("Dialogs")]
    [SerializeField] private GameObject _removeDialogWrapper;

    [SerializeField] private TMP_Text _errorText;

    [Header("Sprites")]
    [SerializeField] private Sprite _itemLockSprite;
    [SerializeField] private Sprite _itemUserSprite;


    public BallBodyController BallBody { get; private set; }
    public Animator BallAnimator { get; private set; }

    private void Start()
    {
        BallBody = (BallBodyController)FindObjectOfType(typeof(BallBodyController));
        BallAnimator = BallBody.GetComponent<Animator>();
        _errorText.text = "1";
        
        _errorText.text = "2";
        

        _addBallButton.onClick.AddListener(AddBallButtonOnClick);
        _editBallButton.onClick.AddListener(EditBallButtonOnClick);
        _removeBallButton.onClick.AddListener(RemoveBallButtonOnClick);
        _shakeButton.onClick.AddListener(() => { StartCoroutine(ShakeBallButtonOnClick()); });
        _quitButton.onClick.AddListener(QuitButtonOnClick);

        if(PlayerPrefs.GetInt("noAds") == 1)
        {
            _noAdsButton.gameObject.SetActive(false);
        }



        GamePlayEventBus.CurrentBallChanged_Event.AddListener(RefreshBallDropdow);

        //Balls.RemoveAllBalls();


        RefreshBallDropdow();

        _ballsDropdown.onValueChanged.AddListener(delegate {
            BallDropdownChangeHandler(_ballsDropdown);
        });


        BallBody.RenderBall();
        //_ballBody.ShowText();  
    }


    public void RefreshBallDropdow()
    {
        _ballsDropdown.ClearOptions();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach(var ball in BallsStorage.GetAllBalls())
        {
            Sprite image = ball.BallType == BallType.DefaultBall ? _itemLockSprite : _itemUserSprite;
            var option = new Dropdown.OptionData(ball.Name, image);
            options.Add(option);
        }

        
       _ballsDropdown.AddOptions(options);

        for (int i = 0; i < _ballsDropdown.options.Count; i++)
        {
            //_ballsDropdown.options[i].image = _itemLockSprite;
            if (_ballsDropdown.options[i].text == BallsStorage.GetCurrentBallName())
            {
                _ballsDropdown.SetValueWithoutNotify(i);

                var ball = BallsStorage.GetCurrentBall();
                if (ball.BallType == BallType.DefaultBall)
                {
                    _editBallButton.interactable = false;
                    _removeBallButton.interactable = false;
                }
                else
                {
                    _editBallButton.interactable = true;
                    _removeBallButton.interactable = true;
                }
            }
        }
    }


    private void AddBallButtonOnClick()
    {
        CreateBallDialogParams.Action = ActionType.AddBall;
        GameScenesController.LoadSceneAsync(GameScene.CreateBall);
        
    } 
    

    private void EditBallButtonOnClick()
    {
        CreateBallDialogParams.Action = ActionType.EditBall;
        CreateBallDialogParams.BallName = _ballsDropdown.options[_ballsDropdown.value].text;
        GameScenesController.LoadSceneAsync(GameScene.CreateBall); 
    }
    
    
    private void RemoveBallButtonOnClick()
    {
        _removeDialogWrapper.SetActive(true);
    } 
    
    
    IEnumerator ShakeBallButtonOnClick()
    {
        GamePlayEventBus.InvokeBallShakedEvent();

        BallAnimator.SetTrigger("shake");
        yield return new WaitForSeconds(0.5f);
        BallBody.ShowText(false);
    }
    
    
    private void QuitButtonOnClick()
    {
        Application.Quit();
    }


    private void BallDropdownChangeHandler(Dropdown ballsDropdown)
    {
        BallsStorage.SetCurrentBallName(ballsDropdown.options[_ballsDropdown.value].text);
        BallData ball = BallsStorage.GetCurrentBall();

        BallBody.RenderBall();
        BallBody.ShowText(true);
    }
}
