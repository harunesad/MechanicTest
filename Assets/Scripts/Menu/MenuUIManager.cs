using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager menuUI;
    public int menuIndex = 0;
    public int propertyIndex = 0;
    public static string directory = "/SaveData/";
    public static string fileName = "Texture.png";
    public List<GameObject> panels;
    public RenderTexture rt;
    public Texture2D myTexture;
        //= toTexture2D();
    //Texture2D toTexture2D(This RenderTexture rTex)
    //{
    //    Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
    //    RenderTexture.active = rTex;
    //    tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
    //    tex.Apply();
    //    return tex;
    //}
    private void Awake()
    {
        menuUI = this;
    }
    void Start()
    {
        //myTexture.mipmapCount.
        Debug.Log(rt.mipmapCount + " " + myTexture.mipmapCount);
    }
    void Update()
    {
        
    }
    public void Play()
    {
        Save();
        JsonSave.json.scene++;
        PlayerPrefs.SetInt("Scene", JsonSave.json.scene);
        SaveManager.Save(JsonSave.json.data);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Save()
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        tex.Apply();
        //Graphics.CopyTexture(rt, myTexture);
        var bytes = tex.EncodeToPNG();
        var dirPath = Application.persistentDataPath + directory;
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        File.WriteAllBytes(dirPath + fileName, bytes);
    }
}
