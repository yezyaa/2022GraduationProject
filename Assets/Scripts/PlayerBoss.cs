using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoss : MonoBehaviour
{
    float hAxis; // Ű����
    float vAxis; // Ű����
    float xAxis; // ���콺
    float yAxis; // ���콺

    bool jDown; // ���� Ű
    bool fDown; // ���� Ű

    bool isJump; // ���� �Ǻ�
    bool isFireReady = true; // ���� �غ�
    bool isDamage;

    [SerializeField] float playerSpeed; // �÷��̾� �̵� �ӵ�
    [SerializeField] float playerJumpPower; // �÷��̾� ���� �Ŀ�
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

    // Input ���
    void getInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");

        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");
    }

    // �÷��̾� �̵�
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

    // �÷��̾� ����
    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * playerJumpPower, ForceMode.Impulse);
            animator.SetTrigger("doJump");
            isJump = true;
        }
    }

    // �÷��̾� ����
    void Attack()
    {
        if (fDown && isFireReady)
        {
            weapon.WeaponUse();
            animator.SetTrigger("doAttack");
            //Destroy(weapon.intantBullet, 2f); // 2�� �� ����
        }
    }

    // ī�޶� ȸ��
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

    // �浹 �� ȸ�� ���ϰ� ���� ���
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

                //Debug.Log("�÷��̾� ü�� : " + playerHp);
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
        yield return new WaitForSeconds(1f); // 1�� ����

        isDamage = false;
    }
}