using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    // 총알 프리팹
    public GameObject bulletPrefab;

    // 총알 발사 좌표
    public Transform firePos;

    // 총소리 오디오 클립
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
        // 마우스 왼쪽 버튼 클릭 했을 때, 
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
        Destroy(bullet, 5f);
    }
    
}
