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

    public bool isAction = false;  //��ȭ�� �÷��̾� �̵� ����
    public bool IsDo = false; //��ȭ�� ���� �ѹ��� �����Ű�� ���� ����
    public bool IsTalk = false;  //��ȭ �� â���� ���� ����
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
        dialogueGroup.blocksRaycasts = true; //���콺 �̺�Ʈ ����
        IsTalk = true;
        isAction = true; //�÷��̾� ������ ���� //true �϶� ��������

        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count != 0 && IsDo == false) //��ȭ����
        {
            currentSentence = sentences.Dequeue();
            //�ڷ�ƾ
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
        if (talkText.text.Equals(currentSentence)) //��� ���� ��
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

        LightPanel(); //��Ҿ��� ǥ��â ����&����
        DoorOpen(); //é��2�� ���� �ٴڹ� ����

        //if (IsTalk == true && PanelOff == true)
        //{
        //    FloorDoor.transform.Translate(Vector3.right * 3 * Time.deltaTime);
        //    Debug.Log("Door Open");
        //}
    }

    void LightPanel()
    {
        if (IsDo == true && PanelOff == false) //��ȭ�� ������
        {
            //LightPop.SetActive(true); //��� â ��Ÿ��

            Invoke("LPanelOff", 0.5f);
        }

    }
    void LPanelOff()
    {
        if (IsDo == true && PanelOff == false)
        {
            if (Input.GetKey(KeyCode.Space)) //â ����
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
            isAction = false; //�÷��̾� ������ ����.

            //FloorDoor.transform.Translate(Vector3.right * 2 * Time.deltaTime);
            //Debug.Log("Door Open");
        }
    }
}
