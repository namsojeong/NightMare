using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    float createTime=10f;

    public List<GameObject> points = new List<GameObject>();

    private void Start()
    {
        InvokeRepeating("RandomSpawn", 2f, createTime);
    }

    void RandomSpawn()
    {
        int ran = Random.Range(0, points.Count);
        if (points[ran].activeSelf) return;

        points[ran].SetActive(true);
    }
}
