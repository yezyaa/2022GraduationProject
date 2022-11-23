using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStage2Scene : MonoBehaviour
{
    public UnityEngine.UI.Image fade;

    float fades = 1.0f;
    float time = 0;

    void Update()
    {
        time += Time.deltaTime;
        if (fades > 0.0f && time >= 0.1f)
        {
            fades -= 0.1f;
            fade.color = new Color(0, 0, 0, fades);
            time = 0;
        }
        else if (fades <= 0.0f)
        {
            time = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Stage2");
        }
    }
}
