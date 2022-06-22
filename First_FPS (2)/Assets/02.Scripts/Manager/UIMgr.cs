using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour
{
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Á¾·á");
        Application.Quit();
    }

    public void UIClick()
    {
        audioSource.Play();
    }
}
