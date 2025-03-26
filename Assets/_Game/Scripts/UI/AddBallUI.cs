using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class AddBallUI : MonoBehaviour
{
    private const float DIALOG_SPACING = 50f;
    private const float SCREEN_WIDTH = 720f;
    private const float SCREEN_HEIGHT = 1280f;


    [SerializeField] private RectTransform _content;

    [Header("Dialogs")]
    [SerializeField] private RectTransform _nameDialog;
    [SerializeField] private RectTransform _answersDialog;

    [Header("Buttons")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;


    private float _contentTargetPositionX = 0;



    // Start is called before the first frame update
    void Awake()
    {
        _content.sizeDelta = new Vector2(SCREEN_WIDTH * 2 + DIALOG_SPACING, SCREEN_HEIGHT);

        _nextButton.onClick.AddListener(() => {
            if(_nameDialog.GetComponent<AddBallNameUI>().NextButtonClickHandler())
            {
                _contentTargetPositionX = -1 * (SCREEN_WIDTH + DIALOG_SPACING);
            }           
        });
        
        _prevButton.onClick.AddListener(() => {
            _contentTargetPositionX = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(_contentTargetPositionX != _content.anchoredPosition.x)
        {
            changeContentPosition();
        }
    }

    public void Init(ActionType action, string ballName = "") { 
    }

    private void changeContentPosition()
    {
        Vector3 targetPosition = new Vector3(_contentTargetPositionX, _content.anchoredPosition.y, 0f);
        float speed = 1500f * Time.deltaTime;

        _content.anchoredPosition = Vector2.MoveTowards(_content.anchoredPosition, targetPosition, speed);
    }



}
