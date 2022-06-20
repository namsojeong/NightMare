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

    public PlayerState colorState;
    public TMP_Text scoreText;
    private int totalScore;

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
        CreateMonsterPool();

        Transform spawnPointGroup = GameObject.Find("SpawnPointGroup")?.transform;

        //spawnPointGroup?.GetComponentsInChildren<Transform>(points);

        //Transform[] pointArray = spawnPointGroup.GetComponentsInChildren<Transform>(true);

        foreach (Transform item in spawnPointGroup)
        {
            points.Add(item);
        }

        InvokeRepeating("CreateMonster", 2.0f, createTime);
        InvokeRepeating("TimeScore", 1f, 1f);
    }
    private void CreateMonster()
    {
        int idx = Random.Range(0, points.Count);

        GameObject _monster = GetMonsterInPool();

        _monster?.transform.SetPositionAndRotation(points[idx].position, points[idx].rotation);
        _monster?.SetActive(true);
    }
    //public void OffJumpMap()
    //{
    //    jumpMap.SetActive(false);
    //    Invoke("OnJumpMap", 5f);
    //}
    
    //public void OnJumpMap()
    //{
    //    jumpMap.SetActive(true);
    //}
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