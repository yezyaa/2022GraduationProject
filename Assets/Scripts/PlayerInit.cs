using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInit : MonoBehaviour
{
    float hAxis; // 키보드
    float vAxis; // 키보드
    float xAxis; // 마우스
    float yAxis; // 마우스

    bool jDown; // 점프 키
    bool iDown; // 대화 상호작용 키

    bool isJump; // 점프 판별

    [SerializeField] float playerSpeed; // 플레이어 이동 속도
    [SerializeField] float playerJumpPower; // 플레이어 점프 파워

    [SerializeField] Transform characterBody;
    [SerializeField] Transform cameraArm;
    Rigidbody rigid;
    Animator animator;
    public TalkManager talkManager;
    GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = characterBody.GetComponent<Animator>();
    }

    void Update()
    {
        getInput();
        Move();
        Jump();
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
        iDown = Input.GetButtonDown("Interation");
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
    }
}