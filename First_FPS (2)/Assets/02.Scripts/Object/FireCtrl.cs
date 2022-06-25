using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    // �Ѿ� ������
    public GameObject bulletPrefab;

    [SerializeField]
    float power = 30f;

    // �Ѿ� �߻� ��ǥ
    public Transform firePos;

    // �ѼҸ� ����� Ŭ��
    public AudioClip fireSfx;

    private new AudioSource audio;

    [SerializeField]
    private ParticleSystem flash;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ���� ��, 
        if( Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;

            Debug.DrawLine(firePos.position, firePos.forward, Color.red);

            if (Physics.Raycast(firePos.position, firePos.forward, out hit))
            {
                if (hit.collider.gameObject != null)
                {
                    Fire();
                    flash.Play();
                }
            }
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * power;
        audio.PlayOneShot(fireSfx, 1.0f);
        Destroy(bullet, 5f);
    }
    
}
