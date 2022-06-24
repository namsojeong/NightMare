using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GallTutorial : MonoBehaviour
{
    [SerializeField]
    ParticleSystem gallParticle;
    [SerializeField]
    Image select;

    MeshRenderer meshRenderer;
    AudioSource audioSource;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BALL"))
        {
            GameMg.Instance().ReturnMonster(collision.gameObject);
            audioSource.Play();
            gallParticle.Play();
            CorrectGall();
        }
    }

    void CorrectGall()
    {

    }
}
