using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyX : MonoBehaviour
{
    [SerializeField] float delta; // ��(��)�� �̵� ������ �ִ밪
    [SerializeField] float enemySpeed; // �̵� �ӵ�

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
        vec.x = enemyPos.x;
        vec.y = transform.position.y;
        vec.z = transform.position.z;
        vec.x += delta * Mathf.Sin(Time.time * enemySpeed);
        transform.position = vec;
    }
}
