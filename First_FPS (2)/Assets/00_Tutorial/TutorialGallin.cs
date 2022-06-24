using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public enum State
{
    INTRO,
    SHOOT,
    COLL,
    GALLIN
}
public class TutorialGallin : MonoBehaviour
{
    [SerializeField]
    Text ex;

    [SerializeField]
    GameObject exitPanel;
    
    [SerializeField]
    GameObject overPanel;

    bool isMouse =false;
    public State state = State.INTRO;

    public static TutorialGallin instance;

    EventParam eventParam = new EventParam();
    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EventManager.StartListening("ISTUTORIALBALL", TouchBall);
    }
    private void Start()
    {
        state = State.INTRO;
        StartCoroutine(StateAction());
    }
    private void OnDestroy()
    {
        EventManager.StopListening("ISTUTORIALBALL", TouchBall);
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            exitPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if(Input.GetMouseButtonDown(0))
        {
            isMouse = true;
        }
        else 
            isMouse = false;
    }
    public IEnumerator StateAction()
    {
        string s;
        Debug.Log(state);
        switch (state)
        {
            case State.INTRO:
                s = "�ȳ��ϼ��� ���ο� ���� �⺻���� ������ �帮�ڽ��ϴ�.";
                Explain(s);
                yield return new WaitForSeconds(1f);
                state = State.SHOOT;
                StartCoroutine(StateAction());
                break;
            case State.SHOOT:
                s = "���� ��Ŭ���� ����������. �׷��� �Ѿ�(�񴰹��)�� �����ϴ�.";
                Explain(s);
                while(!isMouse)
                {
                }
                state = State.COLL;
                StartCoroutine(StateAction());
                break;
            case State.COLL:
                s = "���� �Ѿ��� ���纸����!";
                Explain(s);
                break;
            case State.GALLIN:
                s = "�� ���� �Ѿ˷� ���� �о ���� �׵θ� �ȿ� �־����.";
                Explain(s);
                break;
        }
    }
    void TouchBall(EventParam eventParam)
    {
        state = State.GALLIN;
        StartCoroutine(StateAction());
    }

   public void IsGameOver()
    {
        StartCoroutine(Over());   
    }

    IEnumerator Over()
    {
        yield return new WaitForSeconds(5f);
        overPanel.SetActive(true);
        SceneMg.Instance().ChangeScene("Main");
    }
    private void ResetStr()
    {
        ex.text = string.Format("");
    }
    void Explain(string str)
    {
            ResetStr();
        ex.DOText(str, 1f);
        
    }
    
}
