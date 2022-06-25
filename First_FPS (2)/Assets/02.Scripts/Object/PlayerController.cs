using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float rotationSpeed = 80.0f;

    public float jumpSpeed = 5;

    private Vector3 dir;

    private float verticalVelocity = 0f;
    public float detailX = 5.0f;
    public float detailY = 5.0f;

    public float rotationX = 0f;
    public float rotationY = 0f;

    public Transform cameraTransform;
    private CharacterController playerCtrl;

    AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip enemyClip;

    EventParam eventParam = new EventParam();
    void Start()
    {
        playerCtrl = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        PlayerMove();
    }

    void PlayerSetPos(EventParam eventParam)
    {
        transform.position = eventParam.vectorParam;
    }

    void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveD = cameraTransform.rotation * new Vector3(h, 0, v);
        dir = new Vector3(moveD.x, 0, moveD.z);
        if (Input.GetButtonDown("Jump"))
        {
            jumpSpeed = 5f;
            if (playerCtrl.isGrounded)
            {
                audioSource.clip = jumpClip;
            audioSource.Play();
            Jump();
            }
        }
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        dir.y = verticalVelocity;
        if(transform.position.y<-200)
        {
            SceneMg.Instance().ChangeScene("GameOver");
        }
        playerCtrl.Move(dir * moveSpeed * Time.deltaTime);
    }

    //Player의 회전을 담당
    void PlayerRotate()
    {
        //좌우 회전
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, transform.eulerAngles.x, 0f);
    }

    void PlayerDie()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        foreach(GameObject monster in monsters)
        {
            monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        }

    }

    void Jump()
    {
            verticalVelocity = jumpSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("DUCKBULLET"))
        {
            Debug.Log("BU");
            jumpSpeed = 10;
            Jump();
            audioSource.clip = enemyClip;
            audioSource.Play();
        }
        
    }
}
