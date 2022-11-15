using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { Slime, Turtle, Bomb, Boss };
    public EnemyType enemyType;
    public enum CurrentState { Idle, Trace, Attack, Dead };
    public CurrentState curState = CurrentState.Idle;

    public float enemyHp;
    public float enemyDamage;
    
    public float traceDist; // 추적 사정거리
    public float attackDist; // 공격 사정거리

    bool isDead = false; // 사망여부

    Rigidbody rigid;
    SphereCollider sphereCollider;
    public Animator animator;
    Transform playerTransform;
    NavMeshAgent nvAgent;
    public Canvas HpCanvas;
    public GameObject damageTextPrefab;
    public Transform enemyPos;
    WaitForSeconds wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = gameObject.GetComponent<NavMeshAgent>();
        HpCanvas = GetComponentInChildren<Canvas>();
        wait = new WaitForSeconds(0.2f);
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
            if (curState == CurrentState.Dead)
            {
                yield break;
            }

            float dist = Vector3.Distance(playerTransform.position, transform.position);

            if (dist <= attackDist)
            {
                curState = CurrentState.Attack;
                animator.SetBool("isAttack", true);
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.Trace;
                animator.SetBool("isWalk", true);
                animator.SetBool("isAttack", false);
            }
            else
            {
                curState = CurrentState.Idle;
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", false);
            }
            yield return wait;
        }
    }

    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.Idle:
                    nvAgent.isStopped = true;
                    break;
                case CurrentState.Trace:
                    nvAgent.destination = playerTransform.position;
                    nvAgent.isStopped = false;
                    break;
                case CurrentState.Attack:
                    break;
            }

            yield return null;
        }
    }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero; // 충돌 시 회전 안하게 막는 기능
    }

    /*public Animator GetAnimator()
    {
        return animator;
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            animator.SetTrigger("doGetHit");
            enemyHp -= bullet.bulletDamage;

            GameObject damageText = Instantiate(damageTextPrefab);
            damageText.transform.position = enemyPos.position;
            damageText.transform.SetParent(HpCanvas.transform); // canvas안에 프리팹 생성되게 함
            damageText.GetComponentInChildren<TextMeshProUGUI>().text = bullet.bulletDamage.ToString();
            //Debug.Log("몬스터 체력 : " + enemyHp);

            if (enemyHp <= 0)
            {
                animator.SetTrigger("doDie");
                Destroy(gameObject, 3);
                gameObject.layer = 11;
            }
        }
    }
}
