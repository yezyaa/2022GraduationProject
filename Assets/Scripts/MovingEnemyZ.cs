using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyZ : MonoBehaviour
{
    [SerializeField] float delta; // ��(��)�� �̵� ������ �ִ밪
    [SerializeField] float alienSpeed; // �̵� �ӵ�

    Vector3 enemyPos; // ������ġ

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
        vec.z += delta * Mathf.Sin(Time.time * alienSpeed);
        transform.position = vec;
    }
}
