using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove player;
    public GameObject enemy;
    Transform pos;
    CapsuleCollider cc;
    Animator a;
    int enemyCount = 0;
    private void Awake()
    {
        player = this;
        pos = transform.GetChild(transform.childCount - 1);
        cc = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * 7;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * 7;
        transform.Translate(h, 0, v);
        if ((h != 0 || v != 0) && BaseEnemySound.baseEnemy.myAudio.isPlaying)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("SoldierEnemy").Length;
            if (enemyCount == 0)
            {
                EnemySpawn();
                //enemy.transform.position = pos.position;
                //enemy.SetActive(true);
            }
        }
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    if (a.GetBool("Flexure") == false)
        //    {
        //        cc.height = 1.5f;
        //        cc.center = new Vector3(0, -.25f, 0);
        //    }
        //    else
        //    {
        //        cc.height = 2;
        //        cc.center = new Vector3(0, 0, 0);
        //    }
        //}
    }
    public void EnemySpawn()
    {
        var newEnemy = Instantiate(enemy, pos.position, Quaternion.identity);
        newEnemy.transform.LookAt(transform.position);
    }
    //private void OnParticleCollision(GameObject other)
    //{
    //    Debug.Log("as");
    //}
}
