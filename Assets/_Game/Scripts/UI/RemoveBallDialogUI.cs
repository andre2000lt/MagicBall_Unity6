using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveBallDialogUI : MonoBehaviour
{
    [SerializeField] private Button _submitButton;
    [SerializeField] private Button _cancelButton;

    [SerializeField] private MainUI _mainUI;



    private void OnEnable()
    {
        _submitButton.onClick.AddListener(SubmitButtonClickHandler);
        _cancelButton.onClick.AddListener(CancelButtonClickHandler);
    }


    public void SubmitButtonClickHandler()
    {
        BallsStorage.RemoveCurrentBall();
        _mainUI.RefreshBallDropdow();
        gameObject.SetActive(false);
        _mainUI.BallBody.RenderBall();
    }


    public void CancelButtonClickHandler()
    {
        gameObject.SetActive(false);
    }
}


