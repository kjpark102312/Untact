using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoSingleton<TalkManager>
{
    [SerializeField]
    private Text talkText;
    [SerializeField]
    private GameObject talkPanel;

    public GameObject scanObject;

    public bool isAction = false;
    private int talkIndex;

    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        //Debug.Log("addlewaflef");
        talkData.Add(1000, new string[] { "ㅎㅇㅎㅇ", "반갑고" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        //Debug.Log("addlewaflef");
        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public void Action(GameObject scanObj)
    {
        //Debug.Log("addlewaflef");
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        talkPanel.SetActive(isAction);
        //접촉한 물체 관련
    }

    // 플레이어랑 npc랑 충돌
    // npc istriggerenter
    // Acton(npc의 게임오브젝트+)
    // Action에서 넣은 npc의 정보를 수집해
    // 말을 할수 있게 됨

    void Talk(int id, bool isNpc)
    {
        Debug.Log(id);   
        string talkData = GetTalk(id, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            return;
        }

        if(isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isAction = true;
        talkIndex++;
    }

}
