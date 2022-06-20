using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSc : MonoBehaviour
{
    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void FloorColorChange(Color fc)
    {
        meshRenderer.material.color = fc;
    }
}
