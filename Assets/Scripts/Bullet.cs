using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage;
    /*public float lifeTime = 2f;

    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 2f); // 2檬 第 昏力
        }
        else if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "EnemySlime" ||
                 other.gameObject.tag == "EnemyTurtle" ||
                 other.gameObject.tag == "EnemyBomb" ||
                 other.gameObject.tag == "EnemyBoss")
        {
            Destroy(gameObject);
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 2f); // 2檬 第 昏力
        }
        else if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "EnemySlime" ||
                 collision.gameObject.tag == "EnemyTurtle" ||
                 collision.gameObject.tag == "EnemyBomb" ||
                 collision.gameObject.tag == "EnemyBoss")
        {
            Destroy(gameObject);
        }
    }*/
}
