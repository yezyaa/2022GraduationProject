using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour //, IPointerDownHandler
{
    public GameObject talkPanel;
    //public PlayerMove player;
    //public GameObject LightPop;
    //public GameObject FloorDoor;

    public Text talkText;   //dialogue text
    public GameObject nextText;
    public CanvasGroup dialogueGroup;

    public Queue<string> sentences;
    private string currentSentence;

    //Animator anim;

    public float typingSpeed = 0.5f;
    private bool isTyping;

    public bool isAction = false;  //대화시 플레이어 이동 제어
    public bool IsDo = false; //대화를 최초 한번만 실행시키기 위한 변수
    public bool IsTalk = false;  //대화 후 창띄우기 위한 변수
    public bool PanelOff = false;

    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
        //anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        //LightPop.SetActive(false);
    }

    public void OnDialogue(string[] lines)
    {
        sentences.Clear();
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        dialogueGroup.alpha = 1;
        dialogueGroup.blocksRaycasts = true; //마우스 이벤트 감지
        IsTalk = true;
        isAction = true; //플레이어 움직임 제어 //true 일때 못움직임

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0 && IsDo == false) //대화실행
        {
            currentSentence = sentences.Dequeue();
            //코루틴
            isTyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentence));
        }
        else if (IsTalk == true)
        {
            IsDo = true;
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
        }
        else
        {
            //Debug.Log("talk end");
            dialogueGroup.alpha = 0;
            dialogueGroup.blocksRaycasts = false;
            //IsDo = true;
        }
    }

    IEnumerator Typing(string line)
    {
        talkText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            talkText.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (talkText.text.Equals(currentSentence)) //대사 한줄 끝
        {
            nextText.SetActive(true);
            isTyping = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //throw new System.NotImplementedException();
            if (!isTyping)
            {
                NextSentence();
            }

        }

        LightPanel(); //등불얻음 표시창 실행&꺼짐
        DoorOpen(); //챕터2로 가는 바닥문 열림

        //if (IsTalk == true && PanelOff == true)
        //{
        //    FloorDoor.transform.Translate(Vector3.right * 3 * Time.deltaTime);
        //    Debug.Log("Door Open");
        //}
    }

    void LightPanel()
    {
        if (IsDo == true && PanelOff == false) //대화가 끝나면
        {
            //LightPop.SetActive(true); //등불 창 나타남

            Invoke("LPanelOff", 0.5f);
        }

    }
    void LPanelOff()
    {
        if (IsDo == true && PanelOff == false)
        {
            if (Input.GetKey(KeyCode.Space)) //창 종료
            {
                //LightPop.SetActive(false);
                PanelOff = true;
                //Debug.Log("panle off");
            }
        }

    }

    void DoorOpen()
    {
        if (IsTalk == true && PanelOff == true)
        {
            isAction = false; //플레이어 움직임 가능.

            //FloorDoor.transform.Translate(Vector3.right * 2 * Time.deltaTime);
            //Debug.Log("Door Open");
        }
    }
}
