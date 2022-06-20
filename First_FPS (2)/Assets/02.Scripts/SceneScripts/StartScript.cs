using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class StartScript : MonoBehaviour
{
    public Button startButton;
    public Button optiontButton;

    public UnityAction action;
    private void Start()
    {
        action = () => OnStartClick();
        startButton.onClick.AddListener(action);
        optiontButton.onClick.AddListener(delegate { OnButtonClick(optiontButton.name); });
    }

    void OnStartClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void OnButtonClick(string str)
    {
        Debug.Log($"Click Button : {str}");
    }
}
