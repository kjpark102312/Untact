using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class User
{
    public float Pos1,Pos2,Pos3;

    //public Vector3 Pos4;
    public void printData()
    {
        Debug.Log("Pos : " + Pos1 + "/" + Pos2 + "/" + Pos3);
    }
}

public class PositionSave : MonoSingleton<PositionSave>
{
    private User user;
    public GameObject player;
    void Start()
    {
        user = new User();
        
        /*user.Pos = player.transform.position.x + "/" + player.transform.position.y + "/" + player.transform.position.z;

        string str = JsonUtility.ToJson(user);
        //Debug.Log("ToJson :" + str);

        User user2 = JsonUtility.FromJson<User>(str);
       // user2.printData();*/
    }



    public void LoadUserData()
    {
        //파일 로드
        string str2 = File.ReadAllText(Application.dataPath + "/TestJson.json");
        

        User user4 = JsonUtility.FromJson<User>(str2);
        player.transform.position = new Vector3(user4.Pos1, user4.Pos2, user4.Pos3);
        user4.printData();
    }

    public void SaveUserData()
    {
        //파일 세이브
        User user3 = new User();
        user3.Pos1 = player.transform.position.x;
        user3.Pos2 = player.transform.position.y;
        user3.Pos3 = player.transform.position.z;
        //user3.Pos4 = player.transform.position;
        user3.printData();

        File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(user3));
    }
}
