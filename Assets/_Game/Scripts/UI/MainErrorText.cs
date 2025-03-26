using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainErrorText : MonoBehaviour
{
    private TMP_Text _textErrorOutput;


    void Awake()
    {
        _textErrorOutput = GetComponent<TMP_Text>();
        GamePlayEventBus.BallShaked_Event += BallShakedEventHandler;

        GamePlayEventBus.AddSentError_Event += AddSentErrorEventHandler;
    }

    private void AddSentErrorEventHandler(string arg1, AdErrorType arg2)
    {
        _textErrorOutput.text = arg1;
    }

    private void BallShakedEventHandler()
    {
        _textErrorOutput.text = "Shaked";
    }
}
