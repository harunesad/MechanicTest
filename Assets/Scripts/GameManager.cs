using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using EZCameraShake;

public class GameManager : MonoBehaviour
{
    public static GameManager game;
    Player player;
    public enum Player
    {
        Idle,
        Walk,
        Run,
        Medicine
    } 
    void StateManager()
    {
        switch (player)
        {
            case Player.Idle:
                PlayerProperties.playerProp.Stamina += Time.deltaTime;
                break;
            case Player.Walk:
                PlayerProperties.playerProp.Stamina -= Time.deltaTime;
                break;
            case Player.Run:
                PlayerProperties.playerProp.Stamina -= Time.deltaTime * 2;
                break;
            case Player.Medicine:
                PlayerProperties.playerProp.BrainHealth += 25;
                break;
            default:
                break;
        }
    }
    public List<GameObject> cams;
    public GameObject glasses;
    public LayerMask secretLayer, terribleLayer;
    public TextMeshProUGUI infoText;
    public List<string> texts;
    public GameObject radarObj;
    int textIndex = 0;
    Material mat;
    private void Awake()
    {
        game = this;
    }
    void Start()
    {
        textIndex = PlayerPrefs.GetInt("TextIndex");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Thrower.thrower.StartThrowing();
        }
        if (Thrower.thrower.isCharging)
        {
            Thrower.thrower.ChargeThrow();
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            Thrower.thrower.ReleaseThrow();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CameraShaker.Instance.ShakeOnce(7f, 7f, .1f, 5f);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (cams[0].activeSelf)
            {
                radarObj.SetActive(true);
            }
            cams[0].SetActive(!cams[0].activeSelf);
            cams[1].SetActive(!cams[1].activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            cams[0].SetActive(!cams[0].activeSelf);
            cams[1].SetActive(!cams[1].activeSelf);
        }
        RaycastHit hit;
        if (Physics.Raycast(glasses.transform.position, glasses.transform.forward, out hit, 10, secretLayer))
        {
            int currentTextIndex = 0;
            if (PlayerPrefs.GetString("Text") != texts[currentTextIndex])
            {
                infoText.text = texts[textIndex];
                textIndex++;
                PlayerPrefs.SetInt("TextIndex", textIndex);
                PlayerPrefs.SetString("Text", infoText.text);
                infoText.DOColor(new Color(0, 0, 0, 1), 2).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        infoText.DOColor(new Color(0, 0, 0, 0), 2).SetEase(Ease.Linear);
                    });
            }
            if (hit.transform.childCount > 0 &&!hit.transform.GetChild(0).gameObject.activeSelf)
            {
                float emissiveIntensity = 10;
                mat = hit.transform.GetComponent<Renderer>().material;
                mat.SetColor("_EmissionColor", mat.color * emissiveIntensity);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetChild(0).gameObject.SetActive(true);
                    mat.SetColor("_EmissionColor", mat.color * 0);
                }
            }
        }
        else if (Physics.Raycast(glasses.transform.position, glasses.transform.forward, out hit, 10, terribleLayer))
        {
            PlayerProperties.playerProp.BrainHealth -= 25;
        }
        else if (mat!= null)
        {
            mat.SetColor("_EmissionColor", mat.color * 0);
        }
        Debug.DrawRay(glasses.transform.position, glasses.transform.forward * 10, Color.red);
    }
}
