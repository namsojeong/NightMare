using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMg : MonoBehaviour
{
    [SerializeField]
    float setTime;
    [SerializeField]
    TMP_Text timeText;
    [SerializeField]
    GameObject gameoverImage;
    [SerializeField]
    private TMP_Text bestScoreText;
    [SerializeField]
    private TMP_Text overScoreText;

    //public GameObject player;
    public GameObject monster;
    public List<Transform> points = new List<Transform>();
    public List<GameObject> monsterPool = new List<GameObject>();

    public float createTime = 3.0f;
    public int maxMonster = 10;
    private int nowMonsterCnt = 0;
        int idx;

    public TMP_Text scoreText;
    private int totalScore;
    private int bestScore;

    private bool isGameOver;
    public bool IsGameOver
    {
        get => isGameOver;
        set
        {
            isGameOver = value;
            if (IsGameOver == true)
            {
                CancelInvoke("CreateMonster");
            }
        }
    }

    private static GameMg instance;

    public static GameMg Instance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<GameMg>();

            if (instance == null)
            {
                GameObject container = new GameObject("GameMg");
                instance = container.AddComponent<GameMg>();
            }
        }
        return instance;
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        timeText.text = setTime.ToString();
        DisplayScore(0);
        bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);

        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        foreach (Transform pos in spawnPointGroup)
        {
            points.Add(pos);
        }
        CreateMonsterPool();
        InvokeRepeating("CreateMonster", 2.0f, createTime);
    }
    private void Update()
    {
        TimeAttack();
    }
    private void CreateMonster()
    {
        if (nowMonsterCnt > 2) return;
        idx = Random.Range(0, points.Count);

        GameObject _monster = GetMonsterInPool();

        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        _monster?.SetActive(true);
        nowMonsterCnt++;
    }

    void TimeAttack()
    {
        if(setTime>0)
        {
            setTime -= Time.deltaTime;
        }
        else if(setTime<=0)
        {
            GameOver();
        }

        timeText.text = string.Format(Mathf.Round(setTime).ToString());
    }

    void CreateMonsterPool()
    {
        for (int i = 0; i < maxMonster; ++i)
        {
            var _monster = Instantiate<GameObject>(monster);

            _monster.name = $"Monster_{i:00}";

            _monster.SetActive(false);

            monsterPool.Add(_monster);
        }
    }

    public void ReturnMonster(GameObject mon)
    {
        mon.SetActive(false);
        monsterPool.Add(mon);
        nowMonsterCnt--;
    }

    public GameObject GetMonsterInPool()
    {
        foreach (var _monster in monsterPool)
        {
            if (_monster.activeSelf == false)
            {
                return _monster;

            }
        }
        return null;
    }

    public void DisplayScore(int score)
    {
        totalScore += score;
        scoreText.text = string.Format($"{totalScore}");

        if (totalScore > bestScore)
        {
            bestScore = totalScore;
            PlayerPrefs.SetInt("BESTSCORE", bestScore);
        }
    }

    private void GameOver()
    {
        gameoverImage.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        overScoreText.text = string.Format($"Gall {totalScore}");
        bestScoreText.text = string.Format($"Best score {PlayerPrefs.GetInt("BESTSCORE"),0}");
    }
}