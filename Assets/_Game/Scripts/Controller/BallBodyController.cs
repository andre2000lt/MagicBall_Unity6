using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallBodyController : MonoBehaviour
{
    private const string DEFAULT_TEXT_KEY = "?";

    [SerializeField] private MeshRenderer _ballBody;
    [SerializeField] private TMP_Text _ballTextOutput;
    [SerializeField] private Animator _ballTextAnimator;

    private string _currentAnswer = DEFAULT_TEXT_KEY;
    private bool _isFirstStart = true;



    public void RenderBall()
    {
        BallData currentBall = BallsStorage.GetCurrentBall();
        Material material = Resources.Load<Material>("Materials/" + currentBall.Material);
        _ballBody.material = material;

        if (_isFirstStart)
        {
            GenerateText();
            _isFirstStart = false;
        }
        else
        {
            ShowText(true);
        }
        
    }


    public void ShowText(bool isDefaultText = true)
    {
        int answerIndex = Random.Range(0, BallsStorage.GetCurrentBall().Answers.Count);
        _currentAnswer = isDefaultText? DEFAULT_TEXT_KEY : BallsStorage.GetCurrentBall().Answers[answerIndex];
        _ballTextAnimator.SetTrigger("scale");

        if (isDefaultText == false)
            AudioPlayer.Instance.PlaySound(SoundClipName.Answer);
    }


    public void GenerateText()
    {
        BallData currentBall = BallsStorage.GetCurrentBall();
        _ballTextOutput.text = _currentAnswer;        
    } 
}
