using UnityEngine;
using System.IO;

public static class SaveManager
{
    public static string directory = "/SaveData/";
    public static string fileName = "SaveRunner.json";
    public static void Save(PlayerData data)
    {
        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dir + fileName, json);
    }
    public static PlayerData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        PlayerData data = new PlayerData();
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.Log("Bulunamadý");
        }
        return data;
    }
}
