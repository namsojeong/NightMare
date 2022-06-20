using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMg : MonoBehaviour
{
    private static SceneMg instance;

    public static SceneMg Instance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<SceneMg>();

            if (instance == null)
            {
                GameObject container = new GameObject("SceneMg");
                instance = container.AddComponent<SceneMg>();
            }
        }
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
