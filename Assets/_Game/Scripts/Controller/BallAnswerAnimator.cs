using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnswerAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] BallBodyController _ball;

    public void ChangeText()
    {
        _ball.GenerateText();
    }

    public void PlayAnsweSound()
    {
        AudioPlayer.Instance.PlaySound(SoundClipName.Answer);
    }
}
