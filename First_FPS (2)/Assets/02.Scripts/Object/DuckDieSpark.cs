using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckDieSpark : MonoBehaviour
{
    ParticleSystem spark;

    private void Awake()
    {
        spark = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        spark.Play();
    }
    private void OnDisable()
    {
        spark?.Stop();
    }
}
