using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] float textUpSpeed; // 텍스트 이동 속도
    [SerializeField] float alphaSpeed; // 텍스트 투명도 변환 속도

    TextMeshProUGUI text;
    Color alpha;
    public Enemy enemy;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        enemy = GameObject.FindObjectOfType<Enemy>().GetComponent<Enemy>();
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
