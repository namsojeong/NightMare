using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDrop : MonoBehaviour
{
    [SerializeField]
    float dropSpeed;

    [SerializeField]
    float deleteTime;

    [SerializeField]
    ParticleSystem spawnParticle;

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
        spawnParticle.Play();
    }

    IEnumerator Playing()
    {
        yield return new WaitForSeconds(deleteTime);
        if(!isTouch)
        GameMg.Instance().ReturnMonster(gameObject);
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

            GameMg.Instance().ReturnMonster(collision.gameObject);
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
    }
}
