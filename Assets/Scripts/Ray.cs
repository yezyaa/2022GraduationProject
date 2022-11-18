using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] float degreePerSecond;

    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * degreePerSecond);
    }
}
