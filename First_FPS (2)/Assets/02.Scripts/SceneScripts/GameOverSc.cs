using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class GameOverSc : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;

    public Button retryButton;
    public Button optiontButton;

    public UnityAction action;
    private void Start()
    {
        action = () => OnRetryButton();
        retryButton.onClick.AddListener(action);
        optiontButton.onClick.AddListener(delegate { OnButtonClick(optiontButton.name); });


    }
    void UpdateScoreText()
    {
        scoreText.text = string.Format($"{PlayerPrefs.GetInt("SCORE"), 0}");
        bestScoreText.text = string.Format($"{PlayerPrefs.GetInt("BESTSCORE"), 0}");
    }
    void OnRetryButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnButtonClick(string str)
    {
        Debug.Log($"Click Button : {str}");
    }
}
