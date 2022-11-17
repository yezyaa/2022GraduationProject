using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int bulletSpeed;

    public Transform bulletLeftPos; // �Ѿ� �������� ������ ��ġ
    public Transform bulletRightPos; // �Ѿ� �������� ������ ��ġ
    public GameObject bullet; // �Ѿ� �������� ������ ����
    public Transform bulletLeftShellPos; // ź�� �������� ������ ��ġ
    public Transform bulletRightShellPos; // ź�� �������� ������ ��ġ
    public GameObject bulletShell; // ź�� �������� ������ ����

    public void WeaponUse()
    {
        StopCoroutine("LeftShot");
        StopCoroutine("RightShot");
        StartCoroutine("LeftShot");
        StartCoroutine("RightShot");
    }

    IEnumerator LeftShot()
    {
        // 1. �Ѿ� �߻�, Instantiate()�Լ��� �Ѿ� �ν��Ͻ�ȭ �ϱ�
        GameObject intantBullet = Instantiate(bullet, bulletLeftPos.position, bulletLeftPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletLeftPos.forward * bulletSpeed;

        yield return null;
        // 2. ź�� ����
        GameObject intantBulletShell = Instantiate(bulletShell, bulletLeftShellPos.position, bulletLeftShellPos.rotation);
        Rigidbody bulletShellRigid = intantBulletShell.GetComponent<Rigidbody>();
        Vector3 bulletShellVec = bulletLeftShellPos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletShellRigid.AddForce(bulletShellVec, ForceMode.Impulse);
        bulletShellRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    IEnumerator RightShot()
    {
        // 1. �Ѿ� �߻�, Instantiate()�Լ��� �Ѿ� �ν��Ͻ�ȭ �ϱ�
        GameObject intantBullet = Instantiate(bullet, bulletRightPos.position, bulletRightPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletRightPos.forward * bulletSpeed;

        yield return null;
        // 2. ź�� ����
        GameObject intantBulletShell = Instantiate(bulletShell, bulletRightShellPos.position, bulletRightShellPos.rotation);
        Rigidbody bulletShellRigid = intantBulletShell.GetComponent<Rigidbody>();
        Vector3 bulletShellVec = bulletRightShellPos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletShellRigid.AddForce(bulletShellVec, ForceMode.Impulse);
        bulletShellRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
