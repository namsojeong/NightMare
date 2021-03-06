using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    ParticleSystem gallParticle;

    MeshRenderer meshRenderer;
    AudioSource audioSource;

    EventParam eventParam = new EventParam();
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>(); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BALL"))
        {
            GameMg.Instance().ReturnMonster(collision.gameObject);
            audioSource.Play();
            gallParticle.Play();
            GameMg.Instance().DisplayScore(1);
            eventParam.intParam = 50;
        }
    }
}
