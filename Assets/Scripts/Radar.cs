using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Image player;
    public GameObject playerObj;
    public List<GameObject> enemyObj;
    public List<float> posX;
    public List<float> posZ;
    public List<Image> enemy;
    int index = 0;
    void Start()
    {
        
    }
    void Update()
    {
        //player.rectTransform.localRotation = Quaternion.Euler(0, 0, playerObj.transform.localEulerAngles.y);
        //posX[0] = -playerObj.transform.position.x + enemyObj[0].transform.position.x;
        //posZ[0] = playerObj.transform.position.z - enemyObj[0].transform.position.z;
        //enemy[0].rectTransform.localPosition = player.rectTransform.localPosition + (new Vector3(-posX[0], -posZ[0], 0) * 20);
        Debug.Log(player.rectTransform.localEulerAngles);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PatrolEnemy"))
        {
            enemyObj[index] = other.gameObject;
            player.rectTransform.localRotation = Quaternion.Euler(0, 0, playerObj.transform.localEulerAngles.y);
            posX[index] = -playerObj.transform.position.x + enemyObj[index].transform.position.x;
            posZ[index] = playerObj.transform.position.z - enemyObj[index].transform.position.z;
            enemy[index].gameObject.SetActive(true);
            enemy[index].rectTransform.localPosition = player.rectTransform.localPosition + (new Vector3(-posX[index], -posZ[index], 0) * 45);
            index++;
        }
        StartCoroutine(RadarClose());
    }
    IEnumerator RadarClose()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < enemyObj.Count; i++)
        {
            enemyObj[i] = null;
        }
        for (int i = 0; i < enemy.Count; i++)
        {
            enemy[i].gameObject.SetActive(false);
            //enemy[i] = null;
        }
        index = 0;
        gameObject.SetActive(false);
    }
}
