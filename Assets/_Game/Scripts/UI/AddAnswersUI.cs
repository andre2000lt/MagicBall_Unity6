
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAnswersUI : MonoBehaviour
{
    [SerializeField] private Transform _formTransform;
    [SerializeField] private GameObject _answerPrefab;

    [SerializeField] private ScrollRect _scroller;
    
    [Header("Buttons")]
    [SerializeField] private Button _addAnswerButton;
    [SerializeField] private Button _removeAnswerButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _saveButton;

    private List<Answer> _answerComponents = new List<Answer>();
    private List<string> _answers = new List<string>();


    private void Awake() {
        ActionType action = CreateBallDialogParams.Action;

        if (action == ActionType.AddBall)
        {
            AddAnswer();
            AddAnswer();
        }
        else if (action == ActionType.EditBall)
        {
            List<string> answers = BallsStorage.GetCurrentBall().Answers;
            foreach (var answer in answers)
            {
                AddAnswer(answer);
            }
        }

        _saveButton.onClick.AddListener(SaveButtonClickHandler);

        // Add Answer Click
        _addAnswerButton.onClick.AddListener(() => {
            AddAnswer();
        });

        // Remove Answer Click
        _removeAnswerButton.onClick.AddListener(() => {
            RemoveAnswer();
        });


        UdateAnswers();
    }


    private void Update() {

    _removeAnswerButton.interactable = (_answerComponents.Count <= 2)? false: true;
       

     _saveButton.interactable = IsEnoughtAnswerCount();
    }


    private void AddAnswer(string answerText = "") {
        GameObject answer = Instantiate(_answerPrefab, Vector3.zero, Quaternion.identity, _formTransform);
        Answer answerComponent = answer.GetComponent<Answer>();
        answerComponent.Init(_answerComponents.Count + 1, answerText);      

        _answerComponents.Add(answerComponent);
        SnapTo(_scroller, answerComponent.GetComponent<RectTransform>());
    }


    private void RemoveAnswer() {
        Answer answerComponent = _answerComponents[_answerComponents.Count - 1];
        _answerComponents.Remove(answerComponent);
        Destroy(answerComponent.gameObject);
    }


    private void UdateAnswers() {
        _answers.Clear();

        foreach (var answerComponent in _answerComponents) {
            _answers.Add(answerComponent.GetText());
        }
    }


    private bool IsEnoughtAnswerCount() {
        if(_answerComponents.Count < 2) 
            return false;

        int count = 0;

        foreach (var answerComponent in _answerComponents) {
            if(answerComponent.IsEmty() != true) 
                count++;
        }

        return count >= 2 ? true : false;
    }


    public static void SnapTo(ScrollRect scroller, RectTransform child )
    {
        Canvas.ForceUpdateCanvases();

        var contentPos = (Vector2)scroller.transform.InverseTransformPoint( scroller.content.position );
        var childPos = (Vector2)scroller.transform.InverseTransformPoint( child.position );
        var endPos = contentPos - childPos;
        // If no horizontal scroll, then don't change contentPos.x
        if( !scroller.horizontal ) endPos.x = contentPos.x;
        // If no vertical scroll, then don't change contentPos.y
        if( !scroller.vertical ) endPos.y = contentPos.y;
        scroller.content.anchoredPosition = endPos;
    }


    private void SaveButtonClickHandler()
    {
        UdateAnswers();

        string ballName = CreateBallDialogParams.BallName;
        string ballMaterial = CreateBallDialogParams.SelectedColorName;

        ActionType action = CreateBallDialogParams.Action;

        //GamePlayEventBus.InvokeBallSavedEvent();

        if (action == ActionType.AddBall) 
        {
            BallData newBallData;
            newBallData = new BallData(ballName, ballMaterial, _answers);
            BallsStorage.AddBall(newBallData);
        }
        else if (action == ActionType.EditBall)
        {
            BallData ballData = BallsStorage.GetCurrentBall();
            ballData.SetBallData(ballName, ballMaterial, _answers);
            BallsStorage.AddBall(ballData);
        }


        GameScenesController.LoadSceneAsync(GameScene.Ball);
      


    }
}
