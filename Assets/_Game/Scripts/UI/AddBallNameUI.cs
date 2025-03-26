using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using UnityEngine.Localization;
using UnityEngine.Localization.Tables;




public class AddBallNameUI : MonoBehaviour
{
    private const int MAX_NAME_SIZE = 30;

    [Header("Title")]
    [SerializeField] private Text _dialogTitle; 
    
    [Header("Input")]
    [SerializeField] private InputField _nameInputField;

    [Header("Toggles")]
    [SerializeField] private GameObject _colorTogglesParent;

    [Header("Buttons")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _cancelButton;
    
    [Header("Windows")]
    [SerializeField] private GameObject _errorWindow;

    [Header("Localization")]
    [SerializeField] private LocalizedStringTable _localizedTable;
    private StringTable _currentLocalesTable;
    public string _selectedColorName { get; private set; } = "2";
    public string _ballName { get; private set; } = "";

    private List<Toggle> _colorToggles = new List<Toggle>();

    private void Awake()
    {
        _currentLocalesTable = _localizedTable.GetTable();
        string _dialogTitleKey = CreateBallDialogParams.Action == ActionType.EditBall ?
            "title_editBall" : "title_createBall";
        _dialogTitle.text = _currentLocalesTable[_dialogTitleKey].Value;

        _cancelButton.onClick.AddListener(() => { SceneManager.LoadScene(GameScene.Ball.ToString()); });


        _nameInputField.onValueChanged.AddListener(delegate { NameInputChangeHandler(); });

        _nameInputField.onValidateInput += ValidateNameInput;

        var colorToggles = _colorTogglesParent.GetComponentsInChildren<Toggle>();
        _colorToggles.AddRange(colorToggles);

        foreach (Toggle colorToggle in _colorToggles)
        {
            colorToggle.onValueChanged.AddListener(delegate
            {
                OnColorToggleStateChange(colorToggle);
            });
        }


        if (CreateBallDialogParams.Action == ActionType.EditBall)
        {
            string ballName = CreateBallDialogParams.BallName;
            BallData ballData = BallsStorage.GetBallDataByName(ballName);

            _nameInputField.text = ballData.Name;
            _nameInputField.interactable = false;

            foreach (Toggle colorToggle in _colorToggles)
            {
                if (colorToggle.name == ballData.Material)
                {
                    colorToggle.isOn = true;
                }
            }
        }
        else if(CreateBallDialogParams.Action == ActionType.AddBall)
        {
            _colorToggles[0].isOn = true;
        }
    }



    public void SetBallName(string ballName)
    {
        _ballName = ballName;
    }


    private void OnColorToggleStateChange(Toggle toggle)
    {
        if (toggle.isOn)
        {
            _selectedColorName = toggle.name;
        }
    }


    private void NameInputChangeHandler()
    {
        _nextButton.interactable = _nameInputField.text.Length > 0;
    }

    private char ValidateNameInput(string input, int charIndex, char addedChar)
    {
        if (input.Length > MAX_NAME_SIZE)
        {
            addedChar = '\0';
        }

        return addedChar;
    }


    
    public bool NextButtonClickHandler()
    {
        if (BallsStorage.IsBallAvailable(_nameInputField.text) && CreateBallDialogParams.Action == ActionType.AddBall)
        {
            _errorWindow.SetActive(true);
            return false;
        }
        CreateBallDialogParams.BallName = _nameInputField.text;
        CreateBallDialogParams.SelectedColorName = _selectedColorName;

        return true;
    }


}



