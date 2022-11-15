using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlien : MonoBehaviour
{
    public float enemyDamage;
    [SerializeField] float delta; // ��(��)�� �̵� ������ �ִ밪
    [SerializeField] float alienSpeed; // �̵� �ӵ�

    Vector3 alienPos; // ������ġ

    void Start()
    {
        alienPos = transform.position;    
    }

    void Update()
    {
        MoveAlien();
    }

    void MoveAlien()
    {
        Vector3 vec = alienPos;
        vec.x += delta * Mathf.Sin(Time.time * alienSpeed);
        transform.position = vec;
    }
}
