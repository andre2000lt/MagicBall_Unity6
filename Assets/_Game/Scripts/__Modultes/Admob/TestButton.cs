using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    [SerializeField] private Button _button;


    void Start()
    {
       _button = GetComponent<Button>();
       _button.onClick.AddListener(() => { SceneManager.LoadScene("AdmobTest"); });
    }
}
