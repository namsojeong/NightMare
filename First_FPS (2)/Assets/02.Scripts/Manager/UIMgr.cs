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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ui.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void CloseUI(GameObject ui)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ui.SetActive(false);
        Time.timeScale = 1.0f;
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
