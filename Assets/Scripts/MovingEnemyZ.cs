using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyZ : MonoBehaviour
{
    [SerializeField] float delta; // 앞(뒤)로 이동 가능한 최대값
    [SerializeField] float alienSpeed; // 이동 속도

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
        vec.x = transform.position.x;
        vec.y = transform.position.y;
        vec.z = enemyPos.z;
        vec.z += delta * Mathf.Sin(Time.time * alienSpeed);
        transform.position = vec;
    }
}
