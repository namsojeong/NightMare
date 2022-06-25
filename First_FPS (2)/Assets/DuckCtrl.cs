using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    IDLE,
    RUN,
    ATTACK,
    FAINT
}

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
    [SerializeField, Header("Particle")]
    ParticleSystem enemyShootP;
    [SerializeField, Header("Particle")]
    ParticleSystem faintParticle;

    NavMeshAgent agent;
    Transform target;
    Animator anim;

    EnemyState state;

    private readonly int hashRun = Animator.StringToHash("IsRun");
    private readonly int hashAttack = Animator.StringToHash("Shoot");
    private readonly int hashFaint = Animator.StringToHash("Faint");
    private readonly int hashDamage = Animator.StringToHash("Damage");

    int hp;
    int maxHP = 100;
    bool isDead = false;

    private void Awake()
    {
        state = EnemyState.IDLE;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ResetEnemy();

        // 몬스터의 상태를 체크하는 코루틴 함수
        StartCoroutine(CheckEnemyState());

        // 상태에 따라 몬스터의 행동을 수행하는 코루틴 함수
        StartCoroutine(EnemyAction());
    }
    private void ResetEnemy()
    {
        hp = maxHP;
    }
    IEnumerator CheckEnemyState()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (isDead) continue;
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
                    agent.isStopped = true;
                    transform.LookAt(target.position);
                    anim.SetBool(hashRun, false);
                    anim.SetBool(hashFaint, false);
                    anim.SetBool(hashAttack, true);
                    BulletSpawn();
                    yield return new WaitForSeconds(0.25f);
                    break;
                case EnemyState.RUN:
                    agent.isStopped = false;
                    anim.SetBool(hashFaint, false);
                    anim.SetBool(hashAttack, false);
                    anim.SetBool(hashRun, true);
                    agent.SetDestination(target.transform.position);
                    break;
                case EnemyState.IDLE:
                    agent.isStopped = true;
                    anim.SetBool(hashFaint, false);
                    anim.SetBool(hashRun, false);
                    anim.SetBool(hashAttack, false);
                    break;
                case EnemyState.FAINT:
                    agent.isStopped = true;
                    anim.SetBool(hashAttack, false);
                    anim.SetBool(hashRun, false);
                    anim.SetBool(hashFaint, true);
                    faintParticle.Play();
                    hp = maxHP;
                    yield return new WaitForSeconds(10f);
                    isDead = false;
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    void BulletSpawn()
    {
        GameObject bullet = Instantiate(dbullet, bulletPos.position, Quaternion.identity);
        enemyShootP.Play();
        bullet.GetComponent<Rigidbody>().velocity = bulletPos.forward * 15.0f;
        Destroy(bullet, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            if (isDead) return;
            Damage();
        }
    }
    void Damage()
    {
        anim.SetBool(hashAttack, false);
        anim.SetBool(hashRun, false);
        anim.SetTrigger(hashDamage);
        hp -= 10;
        if (hp <= 0)
        {
            state = EnemyState.FAINT;
            isDead = true;
        }
    }
}
