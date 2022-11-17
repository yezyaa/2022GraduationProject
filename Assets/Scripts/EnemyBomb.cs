using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    public GameObject enemyBomb;
    public float interval;

    IEnumerator Start()
    {
        while(true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(enemyBomb, transform.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }
}
