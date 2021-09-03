using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;
using DG.Tweening;

public class VoiceTest : MonoBehaviour
{
    private KeywordRecognizer recognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;

    public LayerMask whatIsEnemy;

    public GameObject helpList;
    public GameObject moveGround;
    public Light[] lights;
    public Text[] texts;

    Sequence seq1;

    private bool helpCheck = false;
    private bool lightCheck = false;

    private float temp = 0f;
    public float shortTemp = 1000f;
    public float power = 20f;
    public float radius = 5f;

    public GameObject target;
    

    void Start()
    {
        actions.Add("help", Help);
        actions.Add("move", Move);
        actions.Add("light", LightOnOff);
        actions.Add("go away", GoAway);
        actions.Add("way", Way);
        actions.Add("map", Map);

        seq1 = DOTween.Sequence();

        //if(Microphone.devices.Length <= 0)
        //{
        //    Debug.Log("�� ���д�");
        //}
        //else if(Microphone.devices.Length > 0)
        //{
        //    Debug.Log("�� �����̴�"); 
        //}

        helpCheck = true;
        lightCheck = true;
        //GoAway2(transform.position, radius, power); //�׽�Ʈ�� �ڵ�

        //LightOnOff();

        recognizer = new KeywordRecognizer(actions.Keys.ToArray(),confidence);
        recognizer.OnPhraseRecognized += RecognizedSpeech;
        recognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        //1
        Debug.Log(speech.text); //�νĵ� �ؽ�Ʈ
        //Debug.Log(speech.confidence); //��Ȯ�� �ν� Ȯ�Ǽ��� ô��
        //Debug.Log(speech.phraseStartTime); //������ ��ȭ�� ���۵ȼ���
        //Debug.Log(speech.phraseDuration); //������ ��ȭ�Ǵµ� �ɸ��ð�
        //Debug.Log(speech.semanticMeanings); //�νĵ� ������ �ǹ̷����ǹ�
        actions[speech.text].Invoke();
        //2
        //Action keywordAction;
        //if(actions.TryGetValue(speech.text,out keywordAction))
        //{
        //    keywordAction.Invoke();
        //}
    }

    
    private void Help()
    {
        if(helpCheck)
        {
            helpList.SetActive(true);
            seq1.Append(helpList.transform.DOMoveY(target.transform.position.y,1f));
            //Debug.Log(SaveManager.instance.save.checkList.Count);
            for (int i = 0; i < SaveManager.instance.save.checkList.Count; i++)
            {
                if(SaveManager.instance.save.checkList[i]) // help - 1, light - 2, go away - 3
                {
                    //Debug.Log(i);
                    texts[i].gameObject.SetActive(true);
                    
                } 
            }
        }
        else
        {

            seq1.Append(helpList.transform.DOMoveY(helpList.transform.position.y, 1f)).OnComplete(() =>
            {
                helpList.SetActive(false);
            });
        }   
        helpCheck = !helpCheck;
    }
    public void Test()
    {
        Help();
    }

    private void Move()
    {
        StartCoroutine(MoveTo(moveGround, transform.position)); //�̵��� ������Ʈ�� ��ǥ���� ����
    }
    private void LightOnOff()
    {
        if(!lightCheck)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].enabled = false;
            }
        }
        lightCheck = !lightCheck;
    }
    private void GoAway()
    {
        GoAway2(transform.position, radius, power);
    }

    private void GoAway2(Vector3 center, float radius,float power)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius,whatIsEnemy);
        GameObject enemy;
        Vector3 dir;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            
                temp = Vector3.Distance(hitColliders[i].transform.position, transform.position);

                if(temp <= shortTemp)
                {
                    enemy = hitColliders[i].gameObject;
                    dir = enemy.transform.position - transform.position;
                    enemy.GetComponent<Rigidbody>().AddForce(dir.normalized * power, ForceMode.Impulse);
                    //enemy.transform.Translate(dir * power * Time.deltaTime);
                }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
         //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void Way()
    {
        //�� �˷������
    }
    private void Map()
    {
        //�ʿ���
    }

    IEnumerator MoveTo(GameObject a, Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = a.transform.position;
        while (true)
        {
            count += Time.deltaTime;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }
    //�̱��� ������ �� �Ѿ�� ���� ����
    private void OnDestroy()
    {
        if(recognizer != null && recognizer.IsRunning)
        {
            recognizer.Stop();
            recognizer.Dispose();
        }
    }

    private void OnApplicationQuit()
    {
        if(recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= RecognizedSpeech;
            recognizer.Stop();
            recognizer.Dispose();
        }
    }
    //private void Update()
    //{
       
    //}
}
