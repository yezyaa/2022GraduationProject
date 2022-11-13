using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 2f); // 2�� �� ����
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
