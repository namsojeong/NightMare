using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeItem : MonoBehaviour
{
    [SerializeField]
    ParticleSystem getParticle;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMg.Instance().UpTime(10);
            StartCoroutine(GetItem());
        }
    }
    IEnumerator GetItem()
    {
        getParticle.Play();
        audioSource.Play();
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
    }


}
