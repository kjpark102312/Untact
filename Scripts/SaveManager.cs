using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Save
{
    public List<bool> checkList = new List<bool>();
}


public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public Save save;

    [SerializeField]
    private string savepath; //저장/불러오기 위치
    void Start()
    {
        //Debug.Log(save);
        save = new Save();
        savepath = Application.dataPath + "/Resources/TestJson.json";
        //Debug.Log(save);
        instance = this;
        DataLoad();
    }

    public void DataSave()
    {
        string jsonData = JsonUtility.ToJson(save);
        File.WriteAllText(savepath, jsonData);
        Debug.Log("save : " + save);
    }

    public void DataLoad()
    {
        save = new Save();
        if(File.Exists(savepath))
        {
            string fromJsonData = File.ReadAllText(savepath);
            save = JsonUtility.FromJson<Save>(fromJsonData);
            Debug.Log("있었음 :" + save);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                save.checkList.Add(false);
            }
            Debug.Log("없었음 :" + save);
        }
    }
}
