using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyZ : MonoBehaviour
{
    [SerializeField] float delta; // ��(��)�� �̵� ������ �ִ밪
    [SerializeField] float alienSpeed; // �̵� �ӵ�

    Vector3 enemyPos; // ������ġ
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
