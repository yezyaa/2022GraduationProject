using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    public Text talkText;
    public GameObject scanObject;

    void Update()
    {

    }

    public void TalkAction(GameObject scanObj)
    {
        scanObject = scanObj;
        talkText.text = scanObject.name;
    }
}