using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GamePlayEventBus 
{
    //--- BallShaked_Event ---//

    private static event Action _ballShaked_Event;
    public static event Action BallShaked_Event
    {
        add
        {
            // пытаемся удалить обработчик, затем добавляем снова
            _ballShaked_Event -= value;
            _ballShaked_Event += value;
        }
        remove { _ballShaked_Event -= value; }
    }

    public static void InvokeBallShakedEvent()
    {
        _ballShaked_Event?.Invoke();
    }


    //--- CurrentBallChanged_Event ---//
    public static UnityEvent CurrentBallChanged_Event = new UnityEvent();
/*    private static event Action _currentBallChanged_Event;
    public static event Action CurrentBallChanged_Event
    {
        add
        {
            // пытаемся удалить обработчик, затем добавляем снова
            _currentBallChanged_Event -= value;
            _currentBallChanged_Event += value;
        }
        remove { _currentBallChanged_Event -= value; }
    }

    public static void InvokeCurrentBallChangedEvent()
    {
        _currentBallChanged_Event?.Invoke();
    }*/


    //--- BallSaved_Event ---//

    private static event Action _ballSaved_Event;
    public static event Action BallSaved_Event
    {
        add
        {
            // пытаемся удалить обработчик, затем добавляем снова
            _ballSaved_Event -= value;
            _ballSaved_Event += value;
        }
        remove { _ballSaved_Event -= value; }
    }

    public static void InvokeBallSavedEvent()
    {
        _ballSaved_Event?.Invoke();
    }



    //--- GameStarted_Event ---//

    private static event Action _gameStarted_Event;
    public static event Action GameStarted_Event
    {
        add
        {
            // пытаемся удалить обработчик, затем добавляем снова
            _gameStarted_Event -= value;
            _gameStarted_Event += value;
        }
        remove { _gameStarted_Event -= value; }
    }

    public static void InvokeGameStartedEvent()
    {
        _gameStarted_Event?.Invoke();
    }


    //--- AdSentError_Event ---//

    private static event Action<string, AdErrorType> _addSentError_Event;
    public static event Action<string, AdErrorType> AddSentError_Event
    {
        add
        {
            // пытаемся удалить обработчик, затем добавляем снова
            _addSentError_Event -= value;
            _addSentError_Event += value;
        }
        remove { _addSentError_Event -= value; }
    }

    public static void InvokeAddSentErrorEvent(string errorMessage, AdErrorType errorType)
    {
        _addSentError_Event?.Invoke(errorMessage, errorType);
    }
}
