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

    private PlayerState curColorState;

    EventParam eventParam = new EventParam();
    void Start()
    {
        playerCtrl = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

    }
    void Update()
    {
        PlayerMove();

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.gameObject != null)
            {
                switch(hit.collider.tag)
                {
                    case "PURPLEF":
                        curColorState = PlayerState.PURPLE;
                        eventParam.playerStateParam = curColorState;
                        EventManager.TriggerEvent("HPCOLOR", eventParam);
                        break;
                    case "BLUEF":
                        curColorState = PlayerState.BLUE;
                        eventParam.playerStateParam = curColorState;
                        EventManager.TriggerEvent("HPCOLOR", eventParam);
                        break;
                    case "PINKF":
                        curColorState = PlayerState.PINK;
                        eventParam.playerStateParam = curColorState;
                        EventManager.TriggerEvent("HPCOLOR", eventParam);
                        break;
                    case "YELLOWF":
                        curColorState = PlayerState.YELLOW;
                        eventParam.playerStateParam = curColorState;
                        EventManager.TriggerEvent("HPCOLOR", eventParam);
                        break;
                    case "REDF":
                        curColorState = PlayerState.RED;
                        eventParam.playerStateParam = curColorState;
                        EventManager.TriggerEvent("HPCOLOR", eventParam);
                        break;
                    default:
                        break;
                }
                GameMg.Instance().colorState = curColorState;
            }
        }
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
            Jump();
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
        if (playerCtrl.isGrounded)
            verticalVelocity = jumpSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("DUCKBULLET"))
        {
            jumpSpeed = 10;
            Jump();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JUMPGALL"))
        {
            other.SendMessage("JumpGall");
        }
    }
}
