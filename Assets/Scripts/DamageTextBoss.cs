using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextBoss : MonoBehaviour
{
    [SerializeField] float textUpSpeed; // 텍스트 이동 속도
    [SerializeField] float alphaSpeed; // 텍스트 투명도 변환 속도

    TextMeshProUGUI text;
    Color alpha;
    public Boss boss;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        boss = GameObject.FindObjectOfType<Enemy>().GetComponent<Boss>();
    }

    void Start()
    {
        alpha = text.color;
        Invoke("DestroyObject", 2);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, textUpSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
