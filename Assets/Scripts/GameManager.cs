using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float lerpSpeed;
    public int slimeMaxCount;
    public int turtleMaxCount;

    public Player player;
    public Text playerHpText;
    public Image playerHpBar;

    public Text slimeCountText;
    public Text turtleCountText;

    void Awake()
    {
        slimeCountText.text = slimeCountText.ToString();
        turtleCountText.text = turtleMaxCount.ToString();
    }

    void LateUpdate()
    {
        playerHpText.text = player.playerHp.ToString();
        HpBarFiller();
        slimeCountText.text = slimeMaxCount.ToString();
        turtleCountText.text = turtleMaxCount.ToString();
    }

    void HpBarFiller()
    {
        lerpSpeed = 3f * Time.deltaTime;
        playerHpBar.fillAmount = Mathf.Lerp(playerHpBar.fillAmount, player.playerHp / 100, lerpSpeed);
    }
}
