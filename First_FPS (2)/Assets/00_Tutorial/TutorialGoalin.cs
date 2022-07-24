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
    GOALIN
}
public class TutorialGoalin : MonoBehaviour
{
    [SerializeField]
    Text ex;

    [SerializeField]
    GameObject exitPanel;
    
    [SerializeField]
    GameObject overPanel;
    
    [SerializeField]
    GameObject ball;

    public State state = State.INTRO;
    bool isOver = false;
    public static TutorialGoalin instance;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        state = State.INTRO;
        StartCoroutine(StateAction());
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isOver) return;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            exitPanel.SetActive(true);
            Time.timeScale = 0;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if (state== State.SHOOT)
            {
                state = State.COLL;
                StartCoroutine(StateAction());
            }
        }
    }
    public IEnumerator StateAction()
    {
        string s;
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
                s = "���� ��Ŭ���� ����������. �׷��� �񴰹���� �����ϴ�.";
                Explain(s);
                break;
            case State.COLL:
                ball.SetActive(true);
                s = "���� �Ѿ��� ���纸����!";
                Explain(s);
                break;
            case State.GOALIN:
                s = "�� ���� �Ѿ˷� ���� �о �� �� �Ա��� �־����.";
                Explain(s);
                break;
        }
    }
   public void TouchBall()
    {
        state = State.GOALIN;
        StartCoroutine(StateAction());
    }

   public void IsGameOver()
    {
        isOver = true;
        StartCoroutine(Over());   
    }

    IEnumerator Over()
    {
        overPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneMg.Instance().ChangeScene("Main");
    }
    public void BulletTriger()
    {
        if(state==State.COLL)
        {
            state = State.GOALIN;
            StartCoroutine(StateAction());
        }
    }
    void Explain(string str)
    {
        ex.text = string.Format("");
        ex.DOText(str, 2f);
        
    }
    
}