using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyAlienZ : MonoBehaviour
{
    [SerializeField] float delta; // ��(��)�� �̵� ������ �ִ밪
    [SerializeField] float enemySpeed; // �̵� �ӵ�

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
        vec.z += delta * Mathf.Sin(Time.time * enemySpeed);
        transform.position = vec;
    }
}
