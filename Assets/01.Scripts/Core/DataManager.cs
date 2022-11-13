using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class DataManager : MonoSingleton<DataManager>
{
    private User user;
    public User User {get => user; set => user = value;}

    private string SAVE_PATH = "";
    private const string SAVE_FILE = "/UserFile.Json";

    private void Awake() {
        DontDestroyOnLoad(this);

        SAVE_PATH = Application.dataPath + "/Save";

        if(!Directory.Exists(SAVE_PATH)){
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson();

        InvokeRepeating("SaveFile", 1f, 5f);
    }   

    private void LoadFromJson(){
        User data;

        if(File.Exists(SAVE_PATH + SAVE_FILE)){
            string stringJson = File.ReadAllText(SAVE_PATH + SAVE_FILE);
            data = JsonUtility.FromJson<User>(stringJson);
        }
        else{
            data = new User();
        }

        user = data;

        SaveToJson(data);
    }

    private void SaveToJson<T>(T data){
        string stringJson = JsonUtility.ToJson(data, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILE, stringJson, System.Text.Encoding.UTF8);
    }

    private void SaveFile(){
        SaveToJson(user);
    }

    private void OnApplicationQuit() {
        SaveFile();
    }

}

[CustomEditor(typeof(DataManager))]
public class DataEditor : Editor{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
                
        if(GUILayout.Button("데이터 리셋", GUILayout.Height(25), GUILayout.Width(100))){
            string SAVE_PATH = Application.dataPath + "/Save";
            string SAVE_FILE = "/UserFile.Json";

            User resetData = new User();
            string stringJson = JsonUtility.ToJson(resetData, true);
            File.WriteAllText(SAVE_PATH + SAVE_FILE, stringJson, System.Text.Encoding.UTF8);
        }
    }
}
