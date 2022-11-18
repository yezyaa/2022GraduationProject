using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoss : MonoBehaviour
{
    float hAxis; // 키보드
    float vAxis; // 키보드
    float xAxis; // 마우스
    float yAxis; // 마우스

    bool jDown; // 점프 키
    bool fDown; // 공격 키

    bool isJump; // 점프 판별
    bool isFireReady = true; // 공격 준비
    bool isDamage;

    [SerializeField] float playerSpeed; // 플레이어 이동 속도
    [SerializeField] float playerJumpPower; // 플레이어 점프 파워
    public float playerHp;

    [SerializeField] Transform characterBody;
    [SerializeField] Transform cameraArm;
    Rigidbody rigid;
    Animator animator;
    Weapon weapon;
    Boss boss;
    EnemyAlien enemyAlien;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = characterBody.GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        boss = GameObject.FindWithTag("Enemy").GetComponent<Boss>();
        enemyAlien = GetComponent<EnemyAlien>();
    }

    void Update()
    {
        getInput();
        Move();
        Jump();
        Attack();
        CameraLookAround();
    }

    void FixedUpdate()
    {
        FreezeRotation();
    }

    // Input 기능
    void getInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");

        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");
    }

    // 플레이어 이동
    void Move()
    {
        Vector2 moveInput = new Vector2(hAxis, vAxis);
        bool isMove = moveInput.magnitude != 0;
        animator.SetBool("isMove", isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, cameraArm.right.y, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            if (moveInput.magnitude != 0f)
            {
                characterBody.forward = moveDir;
            }
            transform.position += Vector3.ClampMagnitude(moveDir, 1f) * Time.deltaTime * 5f;
        }
        if (!isFireReady)
        {
            moveInput = Vector2.zero;
        }
    }

    // 플레이어 점프
    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * playerJumpPower, ForceMode.Impulse);
            animator.SetTrigger("doJump");
            isJump = true;
        }
    }

    // 플레이어 공격
    void Attack()
    {
        if (fDown && isFireReady)
        {
            weapon.WeaponUse();
            animator.SetTrigger("doAttack");
            //Destroy(weapon.intantBullet, 2f); // 2초 뒤 삭제
        }
    }

    // 카메라 회전
    void CameraLookAround()
    {
        Vector2 mouseDelta = new Vector2(xAxis, yAxis);
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 355f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    // 충돌 시 회전 안하게 막는 기능
    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
        if (collision.gameObject.tag == "Boss") // && enemy.GetAnimator().GetBool("isAttack")
        {
            if (!isDamage)
            {
                Boss boss = collision.gameObject.GetComponent<Boss>();
                playerHp -= boss.enemyDamage;
                animator.SetTrigger("doGetHit");

                StartCoroutine(onDamage());

                //Debug.Log("플레이어 체력 : " + playerHp);
            }
        }
    }

    IEnumerator onDamage()
    {
        isDamage = true;

        Vector3 reactVec = transform.position - gameObject.transform.position;
        reactVec = reactVec.normalized;
        reactVec += Vector3.up;
        rigid.AddForce(reactVec * 2, ForceMode.Impulse);
        yield return new WaitForSeconds(1f); // 1초 무적

        isDamage = false;
    }
}