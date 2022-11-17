using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyX : MonoBehaviour
{
    [SerializeField] float delta; // 좌(우)로 이동 가능한 최대값
    [SerializeField] float enemySpeed; // 이동 속도

    Vector3 enemyPos; // 현재위치
    public Enemy enemy;

    void Start()
    {
        enemyPos = transform.position;
    }

    void Update()
    {
        if (enemy.curState == CurrentState.Idle)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 vec;
        vec.x = enemyPos.x;
        vec.y = transform.position.y;
        vec.z = transform.position.z;
        vec.x += delta * Mathf.Sin(Time.time * enemySpeed);
        transform.position = vec;
    }
}
