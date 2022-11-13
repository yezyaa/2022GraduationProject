using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int bulletSpeed;

    public Transform bulletPos; // �Ѿ� �������� ������ ��ġ
    public GameObject bullet; // �Ѿ� �������� ������ ����
    public Transform bulletShellPos; // ź�� �������� ������ ��ġ
    public GameObject bulletShell; // ź�� �������� ������ ����
    public GameObject intantBullet;

    public void Use()
    {
        StopCoroutine("Shot");
        StartCoroutine("Shot");
    }

    IEnumerator Shot()
    {
        // 1. �Ѿ� �߻�, Instantiate()�Լ��� �Ѿ� �ν��Ͻ�ȭ �ϱ�
        intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * bulletSpeed;

        yield return null;
        // 2. ź�� ����
        GameObject intantBulletShell = Instantiate(bulletShell, bulletShellPos.position, bulletShellPos.rotation);
        Rigidbody bulletShellRigid = intantBulletShell.GetComponent<Rigidbody>();
        Vector3 bulletShellVec = bulletShellPos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletShellRigid.AddForce(bulletShellVec, ForceMode.Impulse);
        bulletShellRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
