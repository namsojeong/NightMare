using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    // ÃÑ¾Ë ÇÁ¸®ÆÕ
    public GameObject bulletPrefab;

    // ÃÑ¾Ë ¹ß»ç ÁÂÇ¥
    public Transform firePos;

    // ÃÑ¼Ò¸® ¿Àµð¿À Å¬¸³
    public AudioClip fireSfx;

    private new AudioSource audio;

    [SerializeField]
    private ParticleSystem flash;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        flash.gameObject.SetActive(false);
    }

    void Update()
    {
        // ¸¶¿ì½º ¿ÞÂÊ ¹öÆ° Å¬¸¯ ÇßÀ» ¶§, 
        if( Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;

            Debug.DrawLine(firePos.position, firePos.forward, Color.red);

            if (Physics.Raycast(firePos.position, firePos.forward, out hit))
            {
                if (hit.collider.gameObject != null)
                {
                    Fire();
                    flash.gameObject.SetActive(true);
                    flash.Play();
                }
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 15.0f;
        audio.PlayOneShot(fireSfx, 1.0f);
        //Destroy(bullet, 2f);
        Debug.Log("½¹");
    }
    
}
