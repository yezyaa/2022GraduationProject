using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float hAxis; // Ű����
    float vAxis; // Ű����
    float xAxis; // ���콺
    float yAxis; // ���콺

    bool jDown; // ���� Ű
    bool fDown; // ���� Ű

    bool isJump; // ���� �Ǻ�
    bool isFireReady = true; // ���� �غ�

    [SerializeField] float playerSpeed; // �÷��̾� �̵� �ӵ�
    [SerializeField] float playerJumpPower; // �÷��̾� ���� �Ŀ�

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
            weapon.Use();
            animator.SetTrigger("doAttack");
            Destroy(weapon.intantBullet, 2f); // 2�� �� ����
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

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero; // �浹 �� ȸ�� ���ϰ� ���� ���
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
