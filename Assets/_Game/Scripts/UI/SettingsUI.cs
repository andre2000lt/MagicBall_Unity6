using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using TMPro;

using UnityEngine.SceneManagement;

public class SettingsUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _closeButton;

    [Header("Dropdown")]
    [SerializeField] private Dropdown _selectLanguageDropdown;

    public static string _savedLocalesKey = "GameLang";
    private List<Locale> _locales = new List<Locale>();
    private List<string> _languages = new List<string>() { "English", "Русский" };


    private void Awake()
    {
        _locales = LocalizationSettings.AvailableLocales.Locales;

        _selectLanguageDropdown.onValueChanged.AddListener(delegate
        {
            OnLanguageDropdownChange(_selectLanguageDropdown);
        });

        _closeButton.onClick.AddListener(() =>
        {
            GameScenesController.LoadSceneAsync(GameScene.Ball);
        });

        FillLanguageDropdown();
    }



    private void FillLanguageDropdown()
    {
        _selectLanguageDropdown.ClearOptions();
        _selectLanguageDropdown.AddOptions(_languages);
        _selectLanguageDropdown.SetValueWithoutNotify(GetSelectedLocalesIndex());
    }


    private int GetSelectedLocalesIndex()
    {
        if (PlayerPrefs.HasKey(_savedLocalesKey))
        {
            return PlayerPrefs.GetInt(_savedLocalesKey);
        }

        Locale selectedLocale = LocalizationSettings.SelectedLocale;
        for (int i = 0; i < _locales.Count; i++)
        {
            if (selectedLocale == _locales[i])
            {
                return i;
            }
        }

        return 0;
    }


    private void OnLanguageDropdownChange(Dropdown dropdown)
    {
        LocalizationSettings.SelectedLocale = _locales[dropdown.value];
        PlayerPrefs.SetInt(_savedLocalesKey, dropdown.value);
    }


    public static void SetSavedlocale()
    {
        if (PlayerPrefs.HasKey(_savedLocalesKey))
        {
            int savedLocalesIndex = PlayerPrefs.GetInt(_savedLocalesKey);
            List<Locale> locales = LocalizationSettings.AvailableLocales.Locales;
            LocalizationSettings.SelectedLocale = locales[savedLocalesIndex];
        }

    }
}
