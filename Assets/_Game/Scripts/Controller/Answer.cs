using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    private InputField _answerText;
    private TMP_Text _answerIdText;
    private int _answerId = 1;



    private void Awake() {
        _answerText = transform.Find("AnswerField/Input").GetComponent<InputField>();
        _answerIdText = transform.Find("AnswerId").GetComponent<TMP_Text>();
    }


    public void Init (int id, string text = "") {
      _answerId = id;
        _answerText.text = text;
      _answerIdText.text = id.ToString();  
    } 


    public string GetText() {
      string text = _answerText.text.Trim();
      return text;
    }


    public bool IsEmty() {
      return _answerText.text.Trim().Length < 1;
    }


    public string Len() {
      return _answerText.text.Trim();
    }
}
