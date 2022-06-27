using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalTutorial : MonoBehaviour
{
    [SerializeField]
    ParticleSystem gallParticle;

    MeshRenderer meshRenderer;
    AudioSource audioSource;

    bool isTouch = false;

    EventParam eventParam = new EventParam();
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BALL"))
        {
            isTouch = true;
            TutorialGoalin.instance.IsGameOver();
            audioSource.Play();
            gallParticle.Play();
        }
    }

 
}
