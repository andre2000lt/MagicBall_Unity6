using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Image _background;

    public static LoadingScreen Instance;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }


    public void Show()
    {
        _background.gameObject.SetActive(true);
    }
    

    public void Hide()
    {
        _background.gameObject.SetActive(false);
    }


    public void SetProgressBarValue(float value)
    {
        _progressBar.value = value;
    }
}
