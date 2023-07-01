using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.IO;
using Unity.VisualScripting;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager uýManager;
    public Image soundBar, staminaBar, brainHealthBar;
    public static string directory = "/SaveData/";
    public static string fileName = "Texture.png";
    public RenderTexture rt;
    Texture2D t2D;
    public Material bilboard;
    private void Awake()
    {
        uýManager = this;
    }
    private void Start()
    {
        Load();
    }
    public void SoundaInc(float inc)
    {
        soundBar.DOFillAmount(soundBar.fillAmount + inc, 1).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                if (soundBar.fillAmount >= 1)
                {
                    PlayerMove.player.EnemySpawn();
                    soundBar.DOFillAmount(0, 1).SetEase(Ease.Linear);
                }
            });
    }
    void Load()
    {
        t2D = new Texture2D(512, 512, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        t2D.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        t2D.Apply();
        string fullPath = Application.persistentDataPath + directory + fileName;
        if (File.Exists(fullPath))
        {
            var bytes = File.ReadAllBytes(fullPath);
            Debug.Log(t2D.width + " " + t2D.height + " " + bytes.Length);
            t2D.LoadImage(bytes);
            t2D.Apply();

            // Assign the texture to this GameObject's material.
            bilboard.mainTexture = t2D;
            //t2D = bytes;
        }
        else
        {
            Debug.Log("Bulunamadý");
        }
    }
}
