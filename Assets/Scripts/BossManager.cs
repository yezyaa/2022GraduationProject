using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    float lerpSpeed;

    public PlayerBoss player;
    public Text playerHpText;
    public Image playerHpBar;

    void LateUpdate()
    {
        playerHpText.text = player.playerHp.ToString();
        HpBarFiller();
    }

    void HpBarFiller()
    {
        lerpSpeed = 3f * Time.deltaTime;
        playerHpBar.fillAmount = Mathf.Lerp(playerHpBar.fillAmount, player.playerHp / 100, lerpSpeed);
    }
}
