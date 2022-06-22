using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    [SerializeField]
    ParticleSystem collParticle;

    MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.collider.CompareTag("GUN") || collision.collider.CompareTag("BULLET") || collision.collider.CompareTag("Player"))
        {
            return;
        }
        collParticle.Play();
        if (collision.collider.CompareTag("BALL"))
        {
            return;
        }
        Destroy(gameObject, 0.2f);
    }
}
