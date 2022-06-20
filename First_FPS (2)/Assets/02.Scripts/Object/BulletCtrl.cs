using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

    }
    private void OnEnable()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("GUN") || collision.collider.CompareTag("BULLET") || collision.collider.CompareTag("Player"))
        {
            return;
        }
        if (collision.collider.CompareTag("BALL"))
        {
            return;
        }
        Debug.Log(collision.collider.tag);
        Destroy(gameObject);
    }
}
