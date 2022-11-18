using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public enum CurrentState { Idle, Trace, Attack, Dead };
    public CurrentState curState = CurrentState.Idle;

    public float enemyHp;
    public float enemyDamage;

    public float traceDist; // 추적 사정거리
    public float attackDist; // 공격 사정거리

    Vector3 slideVec;

    bool isDead = false; // 사망여부

    Rigidbody rigid;
    CapsuleCollider capsuleCollider;
    public Animator animator;
    Transform playerTransform;
    NavMeshAgent nvAgent;
    public Canvas HpCanvas;
    public GameObject damageTextPrefab;
    public Transform enemyPos;
    WaitForSeconds wait;
    public BossManager manager;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
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
                int randomAttack = Random.Range(0, 2);
                switch (randomAttack)
                {
                    case 0:
                        animator.SetBool("isAttack1", true);
                        animator.SetBool("isAttack2", false);
                        animator.SetBool("isSlide", false);
                        break;
                    case 1:
                        animator.SetBool("isAttack2", true);
                        animator.SetBool("isAttack1", false);
                        animator.SetBool("isSlide", false);
                        break;
                    case 2:
                        animator.SetBool("isSlide", true);
                        animator.SetBool("isAttack1", false);
                        animator.SetBool("isAttack2", false);
                        break;
                    default:
                        break;
                }
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.Trace;
                animator.SetBool("isWalk", true);
                animator.SetBool("isAttack1", false);
                animator.SetBool("isAttack2", false);
                animator.SetBool("isSlide", false);
            }
            else
            {
                curState = CurrentState.Idle;
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack1", false);
                animator.SetBool("isAttack2", false);
                animator.SetBool("isSlide", false);
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

            /*GameObject damageText = Instantiate(damageTextPrefab);
            damageText.transform.position = enemyPos.position;
            damageText.transform.SetParent(HpCanvas.transform); // canvas안에 프리팹 생성되게 함
            damageText.GetComponentInChildren<TextMeshProUGUI>().text = bullet.bulletDamage.ToString();*/

            if (enemyHp < 0 && this.tag == "Boss")
            {
                animator.SetBool("doDie", true);
                Destroy(gameObject, 5);
                gameObject.layer = 11;
                SceneManager.LoadScene("End");
            }
        }
    }
}
