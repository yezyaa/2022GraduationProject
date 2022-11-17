using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyAlienZ : MonoBehaviour
{
    [SerializeField] float delta; // 좌(우)로 이동 가능한 최대값
    [SerializeField] float enemySpeed; // 이동 속도

    Vector3 enemyPos; // 현재위치

    void Start()
    {
        enemyPos = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 vec = enemyPos;
        vec.z += delta * Mathf.Sin(Time.time * enemySpeed);
        transform.position = vec;
    }
}
