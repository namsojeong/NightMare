using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum GallkeeperState
{
    IDLE,
    CATCH,
    WALK,
    FAINT
}
public class GallkeeperCtrl : MonoBehaviour
{
    [SerializeField]
    Transform startPos;

    [SerializeField]
    float traceDis;
    [SerializeField]
    private float catchDis;
    [SerializeField]
    private float speed;

    float distance = 1000;

    int damage = 0;

    GallkeeperState state;

    Animator animator;
    private readonly int hashWalk = Animator.StringToHash("Walking");
    private readonly int hashCatch = Animator.StringToHash("Catch");
    private readonly int hashFaint = Animator.StringToHash("Faint");
    private readonly int hashDamage = Animator.StringToHash("Damage");

    NavMeshAgent agent;
    GameObject ball;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        state = GallkeeperState.IDLE;
    }

    private void OnEnable()
    {
        StartCoroutine(GallkeeperAction());
        StartCoroutine(CheckState());

    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit) || Physics.Raycast(transform.position, Vector3.right, out hit) || Physics.Raycast(transform.position, Vector3.left, out hit))
        {
            distance = Vector3.Distance(transform.position, hit.transform.position);
            if (hit.collider.CompareTag("BALL"))
            {
                ball = hit.collider.gameObject;
            }
        }

    }

    IEnumerator CheckState()
    {
        while (true)
        {
            Debug.Log(state);
            if (distance <= catchDis)
            {
                state = GallkeeperState.CATCH;
            }
            else if (distance <= traceDis)
            {
                state = GallkeeperState.WALK;
            }
            else
            {
                if (damage >= 5)
                {
                    state = GallkeeperState.FAINT;
                }
                else
                    state = GallkeeperState.IDLE;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    IEnumerator GallkeeperAction()
    {
        while (true)
        {
            switch (state)
            {
                case GallkeeperState.CATCH:
                    agent.isStopped = true;
                    animator.SetBool(hashCatch, true);
                    animator.SetBool(hashWalk, false);
                    break;
                case GallkeeperState.WALK:
                    animator.SetBool(hashCatch, false);
                    animator.SetBool(hashWalk, true);
                    agent.isStopped = false;
                    agent.SetDestination(ball.transform.position);
                    break;
                case GallkeeperState.FAINT:
                    animator.SetBool(hashWalk, false);
                    animator.SetBool(hashCatch, false);
                    agent.isStopped = true;
                    damage = 0;
                    animator.SetBool(hashFaint, true);
                    yield return new WaitForSeconds(3f);
                    animator.SetBool(hashFaint, false);
                    state = GallkeeperState.IDLE;
                    break;
                case GallkeeperState.IDLE:
                    agent.isStopped = true;
                    animator.SetBool(hashCatch, false);
                    animator.SetBool(hashWalk, false);
                    transform.position = Vector3.MoveTowards(transform.position, startPos.position, Time.deltaTime * speed);
                    break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BULLET"))
        {
            animator.SetBool(hashWalk, false);
            animator.SetBool(hashCatch, false);
            animator.SetTrigger(hashDamage);
            damage++;
        }
    }
}
