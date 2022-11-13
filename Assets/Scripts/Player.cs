using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis; // 키보드
    float vAxis; // 키보드
    float xAxis; // 마우스
    float yAxis; // 마우스

    bool jDown; // 점프 키
    bool fDown; // 공격 키

    bool isJump; // 점프 판별
    bool isFireReady = true; // 공격 준비

    [SerializeField] float playerSpeed; // 플레이어 이동 속도
    [SerializeField] float playerJumpPower; // 플레이어 점프 파워

    Rigidbody rigid;
    Animator animator;
    Weapon weapon;

    [SerializeField] Transform characterBody;
    [SerializeField] Transform cameraArm;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = characterBody.GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
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
            weapon.Use();
            animator.SetTrigger("doAttack");
            Destroy(weapon.intantBullet, 2f); // 2초 뒤 삭제
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

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero; // 충돌 시 회전 안하게 막는 기능
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
        else if (collision.gameObject.tag == "Enemy")
        {

        }
    }
}
