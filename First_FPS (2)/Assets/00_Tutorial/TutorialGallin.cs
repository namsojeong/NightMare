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
                s = "안녕하세요 골인에 대한 기본적인 설명을 드리겠습니다.";
                Explain(s);
                yield return new WaitForSeconds(1f);
                state = State.SHOOT;
                StartCoroutine(StateAction());
                break;
            case State.SHOOT:
                s = "먼저 좌클릭을 눌러보세요. 그러면 총알(비눗방울)이 나갑니다.";
                Explain(s);
                while(!isMouse)
                {
                }
                state = State.COLL;
                StartCoroutine(StateAction());
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
