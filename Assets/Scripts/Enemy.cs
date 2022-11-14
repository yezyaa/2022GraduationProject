using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemyHp;
    public float enemyDamage;
    public enum CurrentState { idle, trace, attack, dead };
    public CurrentState curState = CurrentState.idle;
    
    public float traceDist; // 추적 사정거리
    public float attackDist; // 공격 사정거리

    bool isDead = false; // 사망여부

    Rigidbody rigid;
    SphereCollider sphereCollider;
    public Animator animator;
    Transform playerTransform;
    NavMeshAgent nvAgent;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        nvAgent.destination = playerTransform.position; // 추적 대상의 위치를 설정하면 바로 추적 시작
        StartCoroutine(CheckState());
        StartCoroutine(CheckStateForAction());
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.forward * traceDist, Color.green);
        Debug.DrawRay(transform.position, transform.forward * attackDist, Color.red);
        FreezeRotation();
    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTransform.position, transform.position);

            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
                animator.SetBool("isAttack", true);
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
                animator.SetBool("isWalk", true);
                animator.SetBool("isAttack", false);
            }
            else
            {
                curState = CurrentState.idle;
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", false);
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.isStopped = true;
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;
                    nvAgent.isStopped = false;
                    break;
                case CurrentState.attack:
                    break;
            }

            yield return null;
        }
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero; // 충돌 시 회전 안하게 막는 기능
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            animator.SetTrigger("doGetHit");
            enemyHp -= bullet.bulletDamage;
            Debug.Log("몬스터 체력 : " + enemyHp);
            Destroy(other.gameObject);

            if (enemyHp <= 0)
            {
                animator.SetTrigger("doDie");
                Destroy(gameObject, 3);
                gameObject.layer = 11;
            }
        }
    }


}
