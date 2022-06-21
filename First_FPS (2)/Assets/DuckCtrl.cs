using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DuckCtrl : MonoBehaviour
{
    [SerializeField]
    float maxDis;
    [SerializeField]
    float attackDis = 30;
    [SerializeField]
    float traceDis;
    [SerializeField]
    GameObject dbullet;
    [SerializeField]
    Transform bulletPos;

    NavMeshAgent agent;
    Transform target;
    Animator anim;

    EnemyState state;

    private readonly int hashRun = Animator.StringToHash("IsRun");
    private readonly int hashAttack = Animator.StringToHash("Shoot");

    private void Awake()
    {
        state = EnemyState.IDLE;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // 몬스터의 상태를 체크하는 코루틴 함수
        StartCoroutine(CheckEnemyState());

        // 상태에 따라 몬스터의 행동을 수행하는 코루틴 함수
        StartCoroutine(EnemyAction());
    }
    private void Update()
    {
    }

    IEnumerator CheckEnemyState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance <= attackDis)
            {
                state = EnemyState.ATTACK;
            }
            else if (distance <= traceDis)
            {
                state = EnemyState.RUN;
            }
            else
            {
                state = EnemyState.IDLE;
            }
        }
    }
    IEnumerator EnemyAction()
    {
        while (true)
        {
        target = GameMg.Instance().player.transform;
            switch (state)
            {
                case EnemyState.ATTACK:
                    transform.LookAt(target.position);
                    agent.isStopped = true;
                    anim.SetBool(hashRun, false);
                    anim.SetBool(hashAttack, true);
            yield return new WaitForSeconds(0.25f);
                    BulletSpawn();
                    break;
                case EnemyState.RUN:
                    anim.SetBool(hashAttack, false);
                    agent.SetDestination(target.transform.position);
                    agent.isStopped = false;
                    anim.SetBool(hashRun, true);
                    break;
                case EnemyState.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashRun, false);
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    void BulletSpawn()
    {
        GameObject bullet = Instantiate(dbullet, bulletPos.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = bulletPos.forward * 15.0f;
        Destroy(bullet, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            agent.isStopped = true;
        }
    }
}
