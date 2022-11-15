using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlien : MonoBehaviour
{
    public float enemyDamage;
    [SerializeField] float delta; // 좌(우)로 이동 가능한 최대값
    [SerializeField] float alienSpeed; // 이동 속도

    Vector3 alienPos; // 현재위치

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
