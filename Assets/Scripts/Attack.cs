using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform bulletPos;
    public Transform bullet;
    public int bulletDamage;

    public Camera followCamera;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nexVec = rayHit.point - transform.position;
                transform.LookAt(transform.position + nexVec);
            }
            Fire();
            animator.SetBool("isAttack", true);

        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isAttack", false);
        }
    }

    void Fire()
    {
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation).gameObject;
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50;

        return;
    }
}
