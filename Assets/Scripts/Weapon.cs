using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int bulletSpeed;

    public Transform bulletPos; // 총알 프리팹을 생성할 위치
    public GameObject bullet; // 총알 프리팹을 저장할 변수
    public Transform bulletShellPos; // 탄피 프리팹을 생성할 위치
    public GameObject bulletShell; // 탄피 프리팹을 저장할 변수
    public GameObject intantBullet;

    public void Use()
    {
        StopCoroutine("Shot");
        StartCoroutine("Shot");
    }

    IEnumerator Shot()
    {
        // 1. 총알 발사, Instantiate()함수로 총알 인스턴스화 하기
        intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * bulletSpeed;

        yield return null;
        // 2. 탄피 배출
        GameObject intantBulletShell = Instantiate(bulletShell, bulletShellPos.position, bulletShellPos.rotation);
        Rigidbody bulletShellRigid = intantBulletShell.GetComponent<Rigidbody>();
        Vector3 bulletShellVec = bulletShellPos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        bulletShellRigid.AddForce(bulletShellVec, ForceMode.Impulse);
        bulletShellRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }
}
