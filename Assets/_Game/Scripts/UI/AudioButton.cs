using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Image _audioOffImage;
    [SerializeField] private Image _audioOnImage;

    private Button _audioButton; 


    private void Awake()
    {
        _audioButton = GetComponent<Button>();
        _audioButton.onClick.AddListener(AudioButtonClickHandler);
    }


    private void TogggleAudioState()
    {
        bool isAudioOn = AudioListener.volume > 0f;

        if (isAudioOn) 
        {
            TurnOffAudio();
        }
        else
        {
            TurnOnAudio();
        }
    }


    private void TurnOnAudio()
    {
        _audioOffImage.gameObject.SetActive(true);
        _audioOnImage.gameObject.SetActive(false);
        AudioListener.volume = 1f;
    }
    
    
    private void TurnOffAudio()
    {
        _audioOffImage.gameObject.SetActive(false);
        _audioOnImage.gameObject.SetActive(true);
        AudioListener.volume = 0f;
    }


    #region Listeners
    private void AudioButtonClickHandler()
    {
        TogggleAudioState();
    }
    #endregion
}
