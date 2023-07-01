using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonSave : MonoBehaviour
{
    public int scene;
    public static JsonSave json;
    public PlayerData data;
    public GameObject player;
    public List<Material> colors;
    public List<Material> hairColors;
    private void Awake()
    {
        json = this;
    }
    private void Start()
    {
        StartSave();
        for (int i = 2; i < 8; i++)
        {
            if (data.index[i] >= 0)
            {
                player.transform.GetChild(i).GetChild(data.index[i]).gameObject.SetActive(true);
            }
        }
        player.transform.GetChild(0).GetComponent<Renderer>().material = colors[data.index[0]];
        player.transform.GetChild(1).GetComponent<Renderer>().material = hairColors[data.index[1]];
    }
    #region StartSave
    public void StartSave()
    {
        scene = PlayerPrefs.GetInt("Scene");
        if (PlayerPrefs.GetInt("Scene") == 1 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            data = SaveManager.Load();
        }
    }
    #endregion
}
