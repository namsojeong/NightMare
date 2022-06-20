using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DuckCtrl : MonoBehaviour
{
    [SerializeField]
    float maxDis;
    [SerializeField]
    GameObject dbullet;
    [SerializeField]
    Transform bulletPos;

    NavMeshAgent agent;
    Transform target;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        InvokeRepeating("BulletSpawn", 1f, 2f);
    }
    private void Update()
    {
        target = GameMg.Instance().player.transform;
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance<=maxDis)
        {
            agent.isStopped = false;
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.isStopped = true;
        }
    }

    void BulletSpawn()
    {
        GameObject bullet = Instantiate(dbullet, bulletPos.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = bulletPos.forward* 15.0f;
        Destroy(bullet, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BULLET"))
        {
            agent.isStopped = true;
        }
    }
}
