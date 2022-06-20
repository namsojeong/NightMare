using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDrop : MonoBehaviour
{
    [SerializeField]
    float dropSpeed;

    [SerializeField]
    float deleteTime;


    public bool isTouch = false;

    Material material;
    MeshRenderer mr;
    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        material =mr.material;
    }
    private void OnEnable()
    {
        isTouch = false;
        StartCoroutine(Playing());
    }

    IEnumerator Playing()
    {
        yield return new WaitForSeconds(deleteTime);
        if(!isTouch)
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BULLET"))
        {
            BulletTouch();
        }
        if(collision.collider.CompareTag("GALLIN"))
        {
            if (isTouch) return;
            
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BULLET"))
        {
            BulletTouch();
        }
        if (other.CompareTag("GALLIN"))
        {
            if (isTouch) return;
            GameMg.Instance().DisplayScore(500);
            gameObject.SetActive(false);
        }
    }

    void BulletTouch()
    {
            isTouch = true;
            material.color = Color.white;
    }
}
