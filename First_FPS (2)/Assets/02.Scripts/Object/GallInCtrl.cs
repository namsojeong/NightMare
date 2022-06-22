using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallInCtrl : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material material;
    bool isTrue = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    void JumpGall()
    {
        if (isTrue) return;
        isTrue = true;
        GameMg.Instance().DisplayScore(100);
        material.color = Color.clear;
        StartCoroutine(ResetColor());
    }
    private void OnEnable()
    {
        isTrue = false;
    }
    IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.5f);
        material.color = Color.yellow;
    }
}
