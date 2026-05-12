using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

[Serializable]
public class UserData
{
    public string userID;
    public int userLv;
    public float score;
}
[Serializable]
public class UserDataList
{
    public List<UserData> userList = new List<UserData>();
}

public class JsonTest : MonoBehaviour
{
    public TMP_InputField inputFieldID, inputfieldScore; //입력받은 값
    public TextMeshProUGUI resultText; //텍스트 출력
    string jsonData;
    public UserDataList loadData;
    string savePath = Application.dataPath + "/saveData.json";
    public void SaveJson()
    {
          LoadJson(); //저장된 데이터 먼저 확인
          UserData foundData = loadData.userList.Find(userData => userData.userID == inputFieldID.text);
          //인풋필드 입력값과 같은 아이디가 있는지 확인해서 지역변수 foundData로 넣기
          
        if(foundData == null) //같은게 없으면 목록에 새로 추가
        {
            UserData data = new UserData();
            data.userID = inputFieldID.text;
            data.userLv = 1;
            data.score = float.Parse(inputfieldScore.text); //입력 문자를 float로 변환

            loadData.userList.Add(data);
        }
        else 
        {
            foundData.score = float.Parse(inputfieldScore.text); //기존 데이터를 변경
        }

          jsonData = JsonUtility.ToJson(loadData); //json 형식 문자열로 변환
          File.WriteAllText(savePath, jsonData); //문자열을 파일로 저장
          resultText.text = $"SAVED!"; //저장한 내용 출력
    }
     public void LoadJson()
    {

        if (File.Exists(savePath)) //파일이 있는지 체크
        {
            string loadJson = File.ReadAllText(savePath);  //저장된 파일에서 모든 문자열을 가져옴           
            loadData = JsonUtility.FromJson<UserDataList>(loadJson); //가져온 문자열을 클래스로 변환
            resultText.text = $"LOAD!"; //불러온 내용 출력
        }
        else
        {
            loadData = new UserDataList();
        }



    }
}
