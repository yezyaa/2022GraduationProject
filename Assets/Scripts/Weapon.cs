using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int bulletSpeed;

    public Transform bulletLeftPos; // 총알 프리팹을 생성할 위치
    public Transform bulletRightPos; // 총알 프리팹을 생성할 위치
    public GameObject bullet; // 총알 프리팹을 저장할 변수
    public Transform bulletLeftShellPos; // 탄피 프리팹을 생성할 위치
    public Transform bulletRightShellPos; // 탄피 프리팹을 생성할 위치
    public GameObject bulletShell; // 탄피 프리팹을 저장할 변수

    public void WeaponUse()
    {
        StopCoroutine("LeftShot");
        StopCoroutine("RightShot");
        StartCoroutine("LeftShot");
        StartCoroutine("RightShot");
    }

    IEnumerator LeftShot()
    {
        // 1. 총알 발사, Instantiate()함수로 총알 인스턴스화 하기
        GameObject intantBullet = Instantiate(bullet, bulletLeftPos.position, bulletLeftPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletLeftPos.forward * bulletSpeed;

        yield return null;
        // 2. 탄피 배출
        GameObject intantBulletShell = Instantiate(bulletShell, bulletLeftShellPos.position, bulletLeftShellPos.rotation);
        Rigidbody bulletShellRigid = intantBulletShell.GetComponent<Rigidbody>();
        Vector3 bulletShellVec = bulletLeftShellPos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletShellRigid.AddForce(bulletShellVec, ForceMode.Impulse);
        bulletShellRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    IEnumerator RightShot()
    {
        // 1. 총알 발사, Instantiate()함수로 총알 인스턴스화 하기
        GameObject intantBullet = Instantiate(bullet, bulletRightPos.position, bulletRightPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletRightPos.forward * bulletSpeed;

        yield return null;
        // 2. 탄피 배출
        GameObject intantBulletShell = Instantiate(bulletShell, bulletRightShellPos.position, bulletRightShellPos.rotation);
        Rigidbody bulletShellRigid = intantBulletShell.GetComponent<Rigidbody>();
        Vector3 bulletShellVec = bulletRightShellPos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletShellRigid.AddForce(bulletShellVec, ForceMode.Impulse);
        bulletShellRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
