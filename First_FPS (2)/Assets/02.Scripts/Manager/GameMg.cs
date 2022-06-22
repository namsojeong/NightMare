using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMg : MonoBehaviour
{
    public GameObject player;
    public GameObject monster;
    public List<Transform> points = new List<Transform>();
    public List<GameObject> monsterPool = new List<GameObject>();

    public float createTime = 3.0f;
    public int maxMonster = 10;
    private int nowMonsterCnt = 0;
        int idx;

    public PlayerState colorState;
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

    private void Update()
    {
        Cursor.visible = false;
    }

    private void Start()
    {
        DisplayScore(0);
        bestScore = PlayerPrefs.GetInt("BESTSCORE", 0);

        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        foreach (Transform pos in spawnPointGroup)
        {
            points.Add(pos);
        }
        CreateMonsterPool();
        InvokeRepeating("CreateMonster", 2.0f, createTime);
        InvokeRepeating("TimeScore", 1f, 1f);
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
        PlayerPrefs.SetInt("SCORE", totalScore);

        if (totalScore > bestScore)
        {
            bestScore = totalScore;
            PlayerPrefs.SetInt("BESTSCORE", bestScore);
        }
    }

    public Vector3 GetPlayerPos()
    {
        return player.transform.position;
    }
    void TimeScore()
    {
        DisplayScore(1);
    }
}