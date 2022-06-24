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

    public State state = State.INTRO;
    bool isOver = false;
    public static TutorialGallin instance;

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
                s = "안녕하세요 골인에 대한 기본적인 설명을 드리겠습니다.";
                Explain(s);
                yield return new WaitForSeconds(1f);
                state = State.SHOOT;
                StartCoroutine(StateAction());
                break;
            case State.SHOOT:
                s = "먼저 좌클릭을 눌러보세요. 그러면 총알(비눗방울)이 나갑니다.";
                Explain(s);
                break;
            case State.COLL:
                s = "공에 총알을 맞춰보세요!";
                Explain(s);
                break;
            case State.GALLIN:
                s = "자 이제 총알로 공을 밀어서 빨간 테두리 안에 넣어보세요.";
                Explain(s);
                break;
        }
    }
   public void TouchBall()
    {
        state = State.GALLIN;
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
            state = State.GALLIN;
            StartCoroutine(StateAction());
        }
    }
    void Explain(string str)
    {
        ex.text = string.Format("");
        ex.DOText(str, 2f);
        
    }
    
}
