using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalInCtrl : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material material;
    bool isTrue = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
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
